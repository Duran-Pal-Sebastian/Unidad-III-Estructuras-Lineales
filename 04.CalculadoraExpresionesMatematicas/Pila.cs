using System;

namespace CalculadoraMatematicas
{
    // Pila genérica: sirve para char (operadores) y double (evaluación).
    public class Pila<T>
    {
        private Nodo cima;

        private class Nodo
        {
            public T Dato;
            public Nodo Siguiente;

            public Nodo(T dato)
            {
                Dato = dato;
            }
        }

        public bool Vacia()
        {
            return cima == null;
        }

        public void Apilar(T dato)
        {
            Nodo nuevo = new Nodo(dato);
            nuevo.Siguiente = cima;
            cima = nuevo;
        }

        public T Desapilar()
        {
            if (Vacia())
                return default(T);

            T dato = cima.Dato;
            cima = cima.Siguiente;

            return dato;
        }

        public T tope()
        {
            if (Vacia())
                return default(T);

            return cima.Dato;
        }
    }
}