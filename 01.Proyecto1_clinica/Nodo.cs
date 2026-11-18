using Proyecto1_Clinica;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_clinica
{
    public class Nodo
    {
        public Paciente Datos;
        public Nodo Siguiente;

        public Nodo(Paciente paciente)
        {
            Datos = paciente;
            Siguiente = null;
        }
    }
}
