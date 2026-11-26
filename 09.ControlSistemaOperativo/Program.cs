using System;
using System.Collections.Generic;

namespace Proyecto9_ControlProcesos
{
    internal class Program
    {
        static GestorProcesos gestor = new GestorProcesos();

        static void Main()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("=== CONTROL DE PROCESOS ===");
                Console.WriteLine("1. Crear proceso");
                Console.WriteLine("2. Eliminar proceso");
                Console.WriteLine("3. Ejecutar proceso (siguiente de la cola)");
                Console.WriteLine("4. Bloquear proceso");
                Console.WriteLine("5. Desbloquear proceso");
                Console.WriteLine("6. Mostrar procesos");
                Console.WriteLine("7. Buscar proceso");
                Console.WriteLine("8. Administrar cola de procesos");
                Console.WriteLine("0. Salir");

                opcion = LeerEntero("Seleccione una opción: ");
                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        Crear();
                        break;
                    case 2:
                        gestor.Eliminar(LeerEntero("PID del proceso (ej. 1 para P01): "));
                        Pausa();
                        break;
                    case 3:
                        gestor.EjecutarSiguiente();
                        Pausa();
                        break;
                    case 4:
                        gestor.Bloquear(LeerEntero("PID del proceso (ej. 1 para P01): "));
                        Pausa();
                        break;
                    case 5:
                        gestor.Desbloquear(LeerEntero("PID del proceso (ej. 1 para P01): "));
                        Pausa();
                        break;
                    case 6:
                        gestor.MostrarProcesos();
                        Pausa();
                        break;
                    case 7:
                        Buscar();
                        break;
                    case 8:
                        AdministrarCola();
                        break;
                    case 0:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Pausa();
                        break;
                }
            } while (opcion != 0);
        }

        // ---------- Lectura segura ----------
        static void Pausa()
        {
            Console.WriteLine();
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }

        static int LeerEntero(string mensaje)
        {
            int valor;
            Console.Write(mensaje);

            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Dato no válido, intenta de nuevo: ");
            }

            return valor;
        }

        static int LeerEnteroEnRango(string mensaje, int min, int max)
        {
            int valor = LeerEntero(mensaje);

            while (valor < min || valor > max)
            {
                valor = LeerEntero($"Debe estar entre {min} y {max}: ");
            }

            return valor;
        }

        static string LeerTexto(string mensaje)
        {
            string texto;

            do
            {
                Console.Write(mensaje);
                texto = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(texto));

            return texto.Trim();
        }

        // ---------- Opciones ----------
        static void Crear()
        {
            string nombre = LeerTexto("Nombre del proceso: ");
            int prioridad = LeerEnteroEnRango("Prioridad (1-5): ", 1, 5);
            int tiempo = LeerEnteroEnRango("Tiempo de ejecución (1-999): ", 1, 999);

            Proceso proceso = gestor.Crear(nombre, prioridad, tiempo);
            Console.WriteLine($"Proceso {proceso.Etiqueta} creado en estado NUEVO.");

            Console.Write("¿Admitirlo ahora a la cola de listos? (s/n): ");
            string respuesta = Console.ReadLine();

            if (respuesta != null && respuesta.Trim().ToLower() == "s")
            {
                gestor.Admitir(proceso);
            }

            Pausa();
        }

        static void Buscar()
        {
            Console.WriteLine("Buscar por:");
            Console.WriteLine("1. PID");
            Console.WriteLine("2. Nombre");
            int tipo = LeerEntero("Opción: ");
            Console.WriteLine();

            if (tipo == 1)
            {
                Proceso proceso = gestor.BuscarPorPid(LeerEntero("PID: "));

                if (proceso == null)
                {
                    Console.WriteLine("No se encontró ningún proceso con ese PID.");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(Proceso.Encabezado());
                    Console.WriteLine(proceso);
                }
            }
            else if (tipo == 2)
            {
                List<Proceso> encontrados = gestor.BuscarPorNombre(LeerTexto("Nombre (o parte): "));

                if (encontrados.Count == 0)
                {
                    Console.WriteLine("No se encontraron procesos con ese nombre.");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(Proceso.Encabezado());
                    foreach (Proceso proceso in encontrados)
                    {
                        Console.WriteLine(proceso);
                    }
                }
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }

            Pausa();
        }

        static void AdministrarCola()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("=== ADMINISTRAR COLA DE PROCESOS ===");
                Console.WriteLine("1. Admitir procesos nuevos (NUEVO -> LISTO)");
                Console.WriteLine("2. Mostrar colas (listos y bloqueados)");
                Console.WriteLine("3. Ejecutar todos los procesos listos (Round Robin)");
                Console.WriteLine($"4. Cambiar quantum (actual: {gestor.Quantum})");
                Console.WriteLine("0. Volver");

                opcion = LeerEntero("Seleccione una opción: ");
                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        gestor.AdmitirNuevos();
                        Pausa();
                        break;
                    case 2:
                        gestor.MostrarColas();
                        Pausa();
                        break;
                    case 3:
                        gestor.EjecutarTodos();
                        Pausa();
                        break;
                    case 4:
                        gestor.Quantum = LeerEnteroEnRango("Nuevo quantum (1-20): ", 1, 20);
                        Console.WriteLine($"Quantum actualizado a {gestor.Quantum}.");
                        Pausa();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Pausa();
                        break;
                }
            } while (opcion != 0);
        }
    }
}