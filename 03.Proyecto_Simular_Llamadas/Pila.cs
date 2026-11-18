using System;

namespace Proyecto3_Simular_Llamadas
{
    
    public class Pila
    {
        private Nodo tope;
        private int cantidad;

        public Pila()
        {
            tope = null;
            cantidad = 0;
        }

        public bool Vacia()
        {
            return tope == null;
        }

        public int Contar()
        {
            return cantidad;
        }

        public void Push(Llamada llamada)
        {
            Nodo nuevo = new Nodo(llamada);
            nuevo.Siguiente = tope;
            tope = nuevo;
            cantidad++;
        }

        public Llamada Pop()
        {
            if (tope == null)
                return null;

            Llamada llamada = tope.Datos;
            tope = tope.Siguiente;
            cantidad--;

            return llamada;
        }

        public Llamada Peek()
        {
            if (tope == null)
                return null;

            return tope.Datos;
        }

        public void Mostrar()
        {
            if (tope == null)
            {
                Console.WriteLine("No hay llamadas.");
                return;
            }

            Console.WriteLine("LLAMADA ACTIVA:");
            Console.WriteLine("  " + tope.Datos);

            Nodo aux = tope.Siguiente;

            if (aux == null)
            {
                Console.WriteLine("No hay llamadas en espera.");
                return;
            }

            Console.WriteLine("EN ESPERA (la primera se reanuda al colgar):");
            int posicion = 1;

            while (aux != null)
            {
                Console.WriteLine($"  {posicion}. {aux.Datos}");
                aux = aux.Siguiente;
                posicion++;
            }
        }
    }
}