using System;

namespace Proyecto9_ControlProcesos
{
    public class Proceso
    {
        public int Pid { get; private set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public EstadoProceso Estado { get; set; }
        public int TiempoEjecucion { get; private set; }   // tiempo total que necesita
        public int TiempoRestante { get; set; }            // lo que le falta por ejecutar

        public Proceso(int pid, string nombre, int prioridad, int tiempoEjecucion)
        {
            Pid = pid;
            Nombre = nombre;
            Prioridad = prioridad;
            Estado = EstadoProceso.Nuevo;
            TiempoEjecucion = tiempoEjecucion;
            TiempoRestante = tiempoEjecucion;
        }

        // P01, P02, P03...
        public string Etiqueta
        {
            get { return "P" + Pid.ToString("00"); }
        }

        public string EstadoTexto
        {
            get { return Estado.ToString().ToUpperInvariant(); }
        }

        public static string Encabezado()
        {
            return "PID".PadRight(6) +
                   "Nombre".PadRight(17) +
                   "Prior.".PadRight(8) +
                   "Estado".PadRight(13) +
                   "Tiempo".PadRight(8) +
                   "Restante";
        }

        public override string ToString()
        {
            string nombre = Nombre.Length > 15 ? Nombre.Substring(0, 15) : Nombre;

            return Etiqueta.PadRight(6) +
                   nombre.PadRight(17) +
                   Prioridad.ToString().PadRight(8) +
                   EstadoTexto.PadRight(13) +
                   TiempoEjecucion.ToString().PadRight(8) +
                   TiempoRestante.ToString();
        }
    }
}