using System;

namespace Proyecto2_Restaurante
{
    public class Cola
    {
        private Nodo frente;
        private Nodo final;

        public Cola()
        {
            frente = null;
            final = null;
        }

        public bool Vacia()
        {
            return frente == null;
        }

        public void Encolar(Cliente cliente)
        {
            Nodo nuevo = new Nodo(cliente);

            if (frente == null)
            {
                frente = nuevo;
                final = nuevo;
            }
            else
            {
                final.Siguiente = nuevo;
                final = nuevo;
            }
        }

        public Cliente Desencolar()
        {
            if (frente == null)
                return null;

            Cliente cliente = frente.Datos;
            frente = frente.Siguiente;

            if (frente == null)
                final = null;

            return cliente;
        }

        public void Mostrar()
        {
            if (frente == null)
            {
                Console.WriteLine("No hay clientes en espera.");
                return;
            }

            Nodo actual = frente;
            int posicion = 1;

            while (actual != null)
            {
                Console.WriteLine($"{posicion}. {actual.Datos}");
                actual = actual.Siguiente;
                posicion++;
            }
        }
    }
}