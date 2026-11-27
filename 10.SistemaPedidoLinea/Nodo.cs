namespace Proyecto10_TiendaEnLinea
{
    public class Nodo<T>
    {
        public T Datos;
        public Nodo<T> Siguiente;

        public Nodo(T datos)
        {
            Datos = datos;
            Siguiente = null;
        }
    }
}