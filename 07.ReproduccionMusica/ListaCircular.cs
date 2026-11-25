using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Proyecto7
{
    public class Listacircular
    {
        private Nodo actual;

        private class Nodo
        {
            public Cancion Datos;
            public Nodo Siguiente;

            public Nodo(Cancion cancion)
            {
                Datos = cancion;
            }
        }

        public void Agregar(Cancion cancion)
        {
            Nodo nuevo = new Nodo(cancion);

            if (actual == null)
            {
                actual = nuevo;
                nuevo.Siguiente = nuevo;
                return;
            }
            Nodo auxiliar = actual;

            while (auxiliar.Siguiente != actual)

                auxiliar = auxiliar.Siguiente;

            auxiliar.Siguiente = nuevo;
            nuevo.Siguiente = actual;
        }

        public void Siguiente()
        {
            if (actual != null)
            {
                actual = actual.Siguiente;
                Console.WriteLine($"Reproduciendo:{actual.Datos}");
            }

        }

        public void Mostrar()
        {
            if (actual == null)
                return;

            Nodo auxialiar = actual;
            do
            {
                Console.Write(auxialiar.Datos);
                auxiliar = auxialiar.Siguiente;
            } while (auxialiar != actual);

        }

    }
}
