using System;
using System.Collections.Generic;

namespace Proyecto10_TiendaEnLinea
{
    public class ListaProductos
    {
        private Nodo<Producto> cabeza;
        private int cantidad;

        public ListaProductos()
        {
            cabeza = null;
            cantidad = 0;
        }

        public int Contar()
        {
            return cantidad;
        }

        public void Agregar(Producto producto)
        {
            Nodo<Producto> nuevo = new Nodo<Producto>(producto);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo<Producto> aux = cabeza;
                while (aux.Siguiente != null)
                {
                    aux = aux.Siguiente;
                }
                aux.Siguiente = nuevo;
            }

            cantidad++;
        }

        public Producto Buscar(int id)
        {
            Nodo<Producto> aux = cabeza;

            while (aux != null)
            {
                if (aux.Datos.Id == id)
                    return aux.Datos;

                aux = aux.Siguiente;
            }

            return null;
        }

        public List<Producto> BuscarPorNombre(string texto)
        {
            List<Producto> resultado = new List<Producto>();
            Nodo<Producto> aux = cabeza;

            while (aux != null)
            {
                if (aux.Datos.Nombre.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    resultado.Add(aux.Datos);

                aux = aux.Siguiente;
            }

            return resultado;
        }

        public List<Producto> BuscarPorCategoria(string texto)
        {
            List<Producto> resultado = new List<Producto>();
            Nodo<Producto> aux = cabeza;

            while (aux != null)
            {
                if (aux.Datos.Categoria.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    resultado.Add(aux.Datos);

                aux = aux.Siguiente;
            }

            return resultado;
        }

        public bool Eliminar(int id)
        {
            if (cabeza == null)
                return false;

            if (cabeza.Datos.Id == id)
            {
                cabeza = cabeza.Siguiente;
                cantidad--;
                return true;
            }

            Nodo<Producto> aux = cabeza;

            while (aux.Siguiente != null && aux.Siguiente.Datos.Id != id)
            {
                aux = aux.Siguiente;
            }

            if (aux.Siguiente == null)
                return false;

            aux.Siguiente = aux.Siguiente.Siguiente;
            cantidad--;
            return true;
        }

        public List<Producto> ObtenerTodos()
        {
            List<Producto> resultado = new List<Producto>();
            Nodo<Producto> aux = cabeza;

            while (aux != null)
            {
                resultado.Add(aux.Datos);
                aux = aux.Siguiente;
            }

            return resultado;
        }

        public int Mostrar(string categoria)
        {
            bool filtrar = !string.IsNullOrWhiteSpace(categoria);
            int mostrados = 0;
            Nodo<Producto> aux = cabeza;

            while (aux != null)
            {
                if (!filtrar ||
                    string.Equals(aux.Datos.Categoria, categoria.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    if (mostrados == 0)
                    {
                        Console.WriteLine(Producto.Encabezado());
                        Console.WriteLine(new string('-', 68));
                    }

                    Console.WriteLine(aux.Datos);
                    mostrados++;
                }

                aux = aux.Siguiente;
            }

            return mostrados;
        }
    }
}