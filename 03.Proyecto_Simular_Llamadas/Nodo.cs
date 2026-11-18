using System;

namespace Proyecto3_Simular_Llamadas
{
    public class Nodo
    {
        public Llamada Datos;
        public Nodo Siguiente;

        public Nodo(Llamada llamada)
        {
            Datos = llamada;
            Siguiente = null;
        }
    }
}