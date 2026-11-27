using System;

namespace Proyecto10_TiendaEnLinea
{
    public class PilaHistorial
    {
        private Nodo<Operacion> tope;
        private int cantidad;

        public PilaHistorial()
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

        public void Apilar(Operacion operacion)
        {
            Nodo<Operacion> nuevo = new Nodo<Operacion>(operacion);
            nuevo.Siguiente = tope;
            tope = nuevo;
            cantidad++;
        }

        public Operacion Desapilar()
        {
            if (tope == null)
                return null;

            Operacion operacion = tope.Datos;
            tope = tope.Siguiente;
            cantidad--;

            return operacion;
        }

        public void Mostrar(int maximo)
        {
            if (tope == null)
            {
                Console.WriteLine("No hay operaciones registradas.");
                return;
            }

            Nodo<Operacion> aux = tope;
            int numero = 1;

            while (aux != null && numero <= maximo)
            {
                Console.WriteLine($"{numero}. {aux.Datos}");
                aux = aux.Siguiente;
                numero++;
            }

            if (aux != null)
            {
                Console.WriteLine($"... y {cantidad - maximo} operaciones más antiguas.");
            }
        }
    }
}