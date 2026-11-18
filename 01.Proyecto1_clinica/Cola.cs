using Proyecto1_clinica;
using System;

namespace Proyecto1_Clinica
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

        public void Encolar(Paciente paciente)
        {
            Nodo nuevo = new Nodo(paciente);

            if (frente == null)
            {
                frente = nuevo;
                final = nuevo;
            }
            else if (paciente.Prioridad > frente.Datos.Prioridad)
            {
                nuevo.Siguiente = frente;
                frente = nuevo;
            }
            else
            {
                
                Nodo aux = frente;

                while (aux.Siguiente != null &&
                       aux.Siguiente.Datos.Prioridad >= paciente.Prioridad)
                {
                    aux = aux.Siguiente;
                }

                nuevo.Siguiente = aux.Siguiente;
                aux.Siguiente = nuevo;

               
                if (nuevo.Siguiente == null)
                    final = nuevo;
            }
        }

        public Paciente Desencolar()
        {
            if (frente == null)
                return null;

            Paciente paciente = frente.Datos;
            frente = frente.Siguiente;

            if (frente == null)
            {
                final = null;
            }

            return paciente;
        }

        public void Mostrar()
        {
            if (frente == null)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            Nodo aux = frente;
            int posicion = 1;

            while (aux != null)
            {
                Console.WriteLine($"{posicion}. {aux.Datos}");
                aux = aux.Siguiente;
                posicion++;
            }
        }
    }
}