using System;

namespace Proyecto1_Clinica
{
    internal class Program
    {
        static Cola pacientes = new Cola();

        static void Main(string[] args)
        {
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("========= CLÍNICA =========");
                Console.WriteLine("1. Registrar Paciente");
                Console.WriteLine("2. Atender Paciente");
                Console.WriteLine("3. Mostrar Pacientes");
                Console.WriteLine("0. Salir");

                opcion = LeerEntero("Opción: ");

                switch (opcion)
                {
                    case 1:
                        Registrar();
                        break;
                    case 2:
                        Atender();
                        break;
                    case 3:
                        pacientes.Mostrar();
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

        static void Registrar()
        {
            int expediente = LeerEntero("Expediente: ");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            int edad = LeerEntero("Edad: ");
            int prioridad = LeerEntero("Prioridad: ");

            Paciente paciente = new Paciente(expediente, nombre, edad, prioridad);
            pacientes.Encolar(paciente);

            Console.WriteLine("Paciente registrado con éxito.");
            Console.ReadKey();
        }

        static void Atender()
        {
            Paciente paciente = pacientes.Desencolar();

            if (paciente == null)
            {
                Console.WriteLine("No hay pacientes.");
            }
            else
            {
                Console.WriteLine("Atendido:");
                Console.WriteLine(paciente);
            }

            Console.ReadKey();
        }
    }
}