using System;
using System.Collections.Generic;

namespace Proyecto9_ControlProcesos
{
    // Lista enlazada simple: tabla con TODOS los procesos del sistema.
    public class ListaProcesos
    {
        private Nodo cabeza;
        private int cantidad;

        public ListaProcesos()
        {
            cabeza = null;
            cantidad = 0;
        }

        public bool Vacia()
        {
            return cabeza == null;
        }

        public int Contar()
        {
            return cantidad;
        }

        public void Agregar(Proceso proceso)
        {
            Nodo nuevo = new Nodo(proceso);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo aux = cabeza;
                while (aux.Siguiente != null)
                {
                    aux = aux.Siguiente;
                }
                aux.Siguiente = nuevo;
            }

            cantidad++;
        }

        public Proceso Buscar(int pid)
        {
            Nodo aux = cabeza;

            while (aux != null)
            {
                if (aux.Datos.Pid == pid)
                    return aux.Datos;

                aux = aux.Siguiente;
            }

            return null;
        }

        public List<Proceso> BuscarPorNombre(string nombre)
        {
            List<Proceso> resultado = new List<Proceso>();
            Nodo aux = cabeza;

            while (aux != null)
            {
                if (aux.Datos.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                    resultado.Add(aux.Datos);

                aux = aux.Siguiente;
            }

            return resultado;
        }

        public List<Proceso> ObtenerPorEstado(EstadoProceso estado)
        {
            List<Proceso> resultado = new List<Proceso>();
            Nodo aux = cabeza;

            while (aux != null)
            {
                if (aux.Datos.Estado == estado)
                    resultado.Add(aux.Datos);

                aux = aux.Siguiente;
            }

            return resultado;
        }

        public bool Eliminar(int pid)
        {
            if (cabeza == null)
                return false;

            if (cabeza.Datos.Pid == pid)
            {
                cabeza = cabeza.Siguiente;
                cantidad--;
                return true;
            }

            Nodo aux = cabeza;

            while (aux.Siguiente != null && aux.Siguiente.Datos.Pid != pid)
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
            if (cabeza == null)
            {
                Console.WriteLine("No hay procesos registrados.");
                return;
            }

            Console.WriteLine(Proceso.Encabezado());
            Console.WriteLine(new string('-', 60));

            Nodo aux = cabeza;
            while (aux != null)
            {
                Console.WriteLine(aux.Datos);
                aux = aux.Siguiente;
            }
        }
    }
}