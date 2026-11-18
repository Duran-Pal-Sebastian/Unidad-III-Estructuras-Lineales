using System;

namespace Proyecto2_Restaurante
{
    internal class Program
    {
        static Cola espera = new Cola();

        static void Main()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("=== Restaurante ===");
                Console.WriteLine("1. Agregar cliente");
                Console.WriteLine("2. Asignar Mesa");
                Console.WriteLine("3. Mostrar espera");
                Console.WriteLine("0. Salir");

                opcion = LeerEntero("Seleccione una opción: ");

                switch (opcion)
                {
                    case 1:
                        Agregar();
                        break;
                    case 2:
                        AsignarMesa();
                        break;
                    case 3:
                        Console.WriteLine();
                        espera.Mostrar();
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

        static void Agregar()
        {
            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine();

            int personas = LeerEntero("Ingrese el número de personas: ");
            while (personas <= 0)
            {
                personas = LeerEntero("Debe ser al menos 1 persona: ");
            }

            espera.Encolar(new Cliente(nombre, personas));

            Console.WriteLine("Cliente agregado.");
            Console.ReadKey();
        }

        static void AsignarMesa()
        {
            Cliente cliente = espera.Desencolar();

            if (cliente == null)
            {
                Console.WriteLine("No hay clientes en espera.");
            }
            else
            {
                Console.WriteLine($"Mesa asignada al cliente {cliente}.");
            }

            Console.ReadKey();
        }
    }
}