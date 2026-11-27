using System;
using System.Collections.Generic;

namespace Proyecto10_TiendaEnLinea
{
s    public class ListaPedidos
    {
        private Nodo<Pedido> cabeza;
        private int cantidad;

        public ListaPedidos()
        {
            cabeza = null;
            cantidad = 0;
        }

        public int Contar()
        {
            return cantidad;
        }

        public void Agregar(Pedido pedido)
        {
            Nodo<Pedido> nuevo = new Nodo<Pedido>(pedido);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo<Pedido> aux = cabeza;
                while (aux.Siguiente != null)
                {
                    aux = aux.Siguiente;
                }
                aux.Siguiente = nuevo;
            }

            cantidad++;
        }

        public Pedido Buscar(int numero)
        {
            Nodo<Pedido> aux = cabeza;

            while (aux != null)
            {
                if (aux.Datos.Numero == numero)
                    return aux.Datos;

                aux = aux.Siguiente;
            }

            return null;
        }

        public List<Pedido> BuscarPorCliente(string texto)
        {
            List<Pedido> resultado = new List<Pedido>();
            Nodo<Pedido> aux = cabeza;

            while (aux != null)
            {
                if (aux.Datos.Cliente.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    resultado.Add(aux.Datos);

                aux = aux.Siguiente;
            }

            return resultado;
        }

        public bool ProductoEnPedidosPendientes(int productoId)
        {
            Nodo<Pedido> aux = cabeza;

            while (aux != null)
            {
                if (aux.Datos.Estado == EstadoPedido.Pendiente)
                {
                    foreach (ItemPedido item in aux.Datos.Productos)
                    {
                        if (item.ProductoId == productoId)
                            return true;
                    }
                }

                aux = aux.Siguiente;
            }

            return false;
        }

        public List<Pedido> ObtenerTodos()
        {
            List<Pedido> resultado = new List<Pedido>();
            Nodo<Pedido> aux = cabeza;

            while (aux != null)
            {
                resultado.Add(aux.Datos);
                aux = aux.Siguiente;
            }

            return resultado;
        }
    }
}