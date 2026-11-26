using System;

namespace Proyecto9_ControlProcesos
{
    public class ColaCircular
    {
        private Proceso[] elementos;
        private int frente;
        private int fin;        
        private int cantidad;

        public ColaCircular(int capacidad)
        {
            elementos = new Proceso[capacidad];
            frente = 0;
            fin = 0;
            cantidad = 0;
        }

        public int Capacidad
        {
            get { return elementos.Length; }
        }

        public bool Vacia()
        {
            return cantidad == 0;
        }

        public bool Llena()
        {
            return cantidad == elementos.Length;
        }

        public int Contar()
        {
            return cantidad;
        }

        public bool Encolar(Proceso proceso)
        {
            if (Llena())
                return false;

            elementos[fin] = proceso;
            fin = (fin + 1) % elementos.Length;
            cantidad++;
            return true;
        }

        public Proceso Desencolar()
        {
            if (Vacia())
                return null;

            Proceso proceso = elementos[frente];
            elementos[frente] = null;
            frente = (frente + 1) % elementos.Length;
            cantidad--;

            return proceso;
        }

        public bool Remover(Proceso objetivo)
        {
            bool encontrado = false;
            int total = cantidad;

            for (int i = 0; i < total; i++)
            {
                Proceso actual = Desencolar();

                if (actual == objetivo)
                    encontrado = true;
                else
                    Encolar(actual);
            }

            return encontrado;
        }

        public void Mostrar()
        {
            if (cantidad == 0)
            {
                Console.WriteLine("(vacía)");
                return;
            }

            for (int i = 0; i < cantidad; i++)
            {
                int indice = (frente + i) % elementos.Length;
                Console.Write(elementos[indice].Etiqueta + " -> ");
            }
            Console.WriteLine();
        }
    }
}