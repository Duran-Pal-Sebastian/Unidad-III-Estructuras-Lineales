namespace Proyecto9_ControlProcesos
{
    public class Nodo
    {
        public Proceso Datos;
        public Nodo Siguiente;

        public Nodo(Proceso proceso)
        {
            Datos = proceso;
            Siguiente = null;
        }
    }
}