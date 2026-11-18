using System;

namespace Proyecto3_Simular_Llamadas
{
    internal class Program
    {
        static Pila llamadas = new Pila();

        static void Main()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("=== Simulador de llamadas ===");
                Console.WriteLine("1. Nueva llamada");
                Console.WriteLine("2. Colgar llamada actual");
                Console.WriteLine("3. Ver llamada actual");
                Console.WriteLine("4. Mostrar todas las llamadas");
                Console.WriteLine("0. Salir");

                opcion = LeerEntero("Seleccione una opción: ");

                switch (opcion)
                {
                    case 1:
                        NuevaLlamada();
                        break;
                    case 2:
                        Colgar();
                        break;
                    case 3:
                        VerActual();
                        break;
                    case 4:
                        Console.WriteLine();
                        llamadas.Mostrar();
                        Console.ReadKey();
                        break;
                    case 0:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 0);
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

        static void NuevaLlamada()
        {
            string contacto = LeerTexto("Nombre del contacto: ");
            string numero = LeerTexto("Número: ");

            Llamada actual = llamadas.Peek();

            if (actual != null)
            {
                Console.WriteLine($"La llamada con {actual.Contacto} pasa a espera.");
            }

            llamadas.Push(new Llamada(contacto, numero));

            Console.WriteLine($"Llamada activa con {contacto}.");
            Console.ReadKey();
        }

        static void Colgar()
        {
            Llamada llamada = llamadas.Pop();

            if (llamada == null)
            {
                Console.WriteLine("No hay llamadas para colgar.");
                Console.ReadKey();
                return;
            }

            TimeSpan duracion = DateTime.Now - llamada.HoraInicio;
            Console.WriteLine($"Llamada con {llamada.Contacto} finalizada. " +
                              $"Duración: {(int)duracion.TotalMinutes:00}:{duracion.Seconds:00}");

            Llamada siguiente = llamadas.Peek();

            if (siguiente != null)
            {
                Console.WriteLine($"Se reanuda la llamada con {siguiente.Contacto}.");
            }
            else
            {
                Console.WriteLine("No quedan llamadas en espera.");
            }

            Console.ReadKey();
        }

        static void VerActual()
        {
            Llamada actual = llamadas.Peek();

            if (actual == null)
            {
                Console.WriteLine("No hay ninguna llamada activa.");
            }
            else
            {
                Console.WriteLine("Llamada activa:");
                Console.WriteLine(actual);
                Console.WriteLine($"Llamadas en espera: {llamadas.Contar() - 1}");
            }

            Console.ReadKey();
        }
    }
}