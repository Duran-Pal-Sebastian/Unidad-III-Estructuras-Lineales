using System;

namespace Proyecto9_ControlProcesos
{
    public class Cola
    {
        private Nodo frente;
        private Nodo final;
        private int cantidad;

        public Cola()
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

        public void Encolar(Proceso proceso)
        {
            Nodo nuevo = new Nodo(proceso);

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

        public Proceso Desencolar()
        {
            if (frente == null)
                return null;

            Proceso proceso = frente.Datos;
            frente = frente.Siguiente;

            if (frente == null)
            {
                final = null;
            }

            cantidad--;
            return proceso;
        }

        // Saca un proceso específico de en medio de la cola.
        public bool Remover(Proceso objetivo)
        {
            if (frente == null)
                return false;

            if (frente.Datos == objetivo)
            {
                Desencolar();
                return true;
            }

            Nodo aux = frente;

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
                Console.WriteLine("(vacía)");
                return;
            }

            Nodo aux = frente;
            while (aux != null)
            {
                Console.Write(aux.Datos.Etiqueta + " -> ");
                aux = aux.Siguiente;
            }
            Console.WriteLine();
        }
    }
}