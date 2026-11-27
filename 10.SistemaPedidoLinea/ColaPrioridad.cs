using System;

namespace Proyecto10_TiendaEnLinea
{
    public class ColaPrioridad
    {
        private Nodo<Pedido> frente;
        private int cantidad;

        public ColaPrioridad()
        {
            frente = null;
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

            if (frente == null || pedido.Prioridad > frente.Datos.Prioridad)
            {
                nuevo.Siguiente = frente;
                frente = nuevo;
            }
            else
            {
                Nodo<Pedido> aux = frente;

                while (aux.Siguiente != null &&
                       aux.Siguiente.Datos.Prioridad >= pedido.Prioridad)
                {
                    aux = aux.Siguiente;
                }

                nuevo.Siguiente = aux.Siguiente;
                aux.Siguiente = nuevo;
            }

            cantidad++;
        }

        public Pedido Desencolar()
        {
            if (frente == null)
                return null;

            Pedido pedido = frente.Datos;
            frente = frente.Siguiente;
            cantidad--;

            return pedido;
        }

        public bool Remover(Pedido objetivo)
        {
            if (frente == null)
                return false;

            if (frente.Datos == objetivo)
            {
                frente = frente.Siguiente;
                cantidad--;
                return true;
            }

            Nodo<Pedido> aux = frente;

            while (aux.Siguiente != null && aux.Siguiente.Datos != objetivo)
            {
                aux = aux.Siguiente;
            }

            if (aux.Siguiente == null)
                return false;

            aux.Siguiente = aux.Siguiente.Siguiente;
            cantidad--;
            return true;
        }

        public void Mostrar()
        {
            if (frente == null)
            {
                Console.WriteLine("(no hay pedidos urgentes)");
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