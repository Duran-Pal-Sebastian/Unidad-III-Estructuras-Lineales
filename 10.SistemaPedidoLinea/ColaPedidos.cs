using System;

namespace Proyecto10_TiendaEnLinea
{
    public class ColaPedidos
    {
        private Nodo<Pedido> frente;
        private Nodo<Pedido> final;
        private int cantidad;

        public ColaPedidos()
        {
            frente = null;
            final = null;
            cantidad = 0;
        }

        public bool Vacia()
        {
            return frente == null;
        }

        public int Contar()
        {
            return cantidad;
        }

        public void Encolar(Pedido pedido)
        {
            Nodo<Pedido> nuevo = new Nodo<Pedido>(pedido);

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

            cantidad++;
        }

        public Pedido Desencolar()
        {
            if (frente == null)
                return null;

            Pedido pedido = frente.Datos;
            frente = frente.Siguiente;

            if (frente == null)
            {
                final = null;
            }

            cantidad--;
            return pedido;
        }

        public bool Remover(Pedido objetivo)
        {
            if (frente == null)
                return false;

            if (frente.Datos == objetivo)
            {
                Desencolar();
                return true;
            }

            Nodo<Pedido> aux = frente;

            while (aux.Siguiente != null && aux.Siguiente.Datos != objetivo)
            {
                aux = aux.Siguiente;
            }

            if (aux.Siguiente == null)
                return false;

            if (aux.Siguiente == final)
            {
                final = aux;
            }

            aux.Siguiente = aux.Siguiente.Siguiente;
            cantidad--;
            return true;
        }

        public void Mostrar()
        {
            if (frente == null)
            {
                Console.WriteLine("(no hay pedidos)");
                return;
            }

            Console.WriteLine(Pedido.Encabezado());
            Console.WriteLine(new string('-', 78));

            Nodo<Pedido> aux = frente;
            while (aux != null)
            {
                Console.WriteLine(aux.Datos);
                aux = aux.Siguiente;
            }
        }
    }
}