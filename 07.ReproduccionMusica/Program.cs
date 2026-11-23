using System;
namespace Proyecto7
{
    internal class Program
    {
        static ListaCircular playlist = 
            new ListaCircular();
        static void Main()
        {
            int opcion;

            do
            {
                console.Clear();

                Console.WriteLine("=====REPRODUCTOR=====");
                Console.WriteLine("1. Agregar canción");
                Console.WriteLine("2. Siguiente");
                Console.WriteLine("3. Mostrar Playlist");
                Console.WriteLine("0. Salir");

                opcion = int.Parse(Console.readLine());

                switch (opcion)
                {
                    case 1
                        Console.Write("Cancion: ");
                        string nombre = Console.ReadLine();

                        Console.Write("Artista: ");
                        string artista = Console.ReadLine();

                        playlist.Agregar(
                            new Cancion(nombre, artista));
                        break;

                    case 2 
                        playlist.Siguiente();
                        Console.Readkey();
                        break;

                    case 3
                        playlist.Mostrar();
                        Console.Readkey();
                        break;


                    
                } 
            } while (opcion != 0);
        }
    }
}