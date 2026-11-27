using System;

namespace Proyecto10_TiendaEnLinea
{
    public class Operacion
    {
        public DateTime Fecha { get; private set; }
        public string Descripcion { get; private set; }

        public Operacion(string descripcion)
        {
            Fecha = DateTime.Now;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return $"[{Fecha:dd/MM/yyyy HH:mm:ss}] {Descripcion}";
        }
    }
}