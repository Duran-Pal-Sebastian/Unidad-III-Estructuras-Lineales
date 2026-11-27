using System;

namespace Proyecto10_TiendaEnLinea
{
    public class ItemPedido
    {
        public int ProductoId { get; private set; }
        public string Nombre { get; private set; }
        public decimal PrecioUnitario { get; private set; }
        public int Cantidad { get; set; }

        public ItemPedido(int productoId, string nombre, decimal precioUnitario, int cantidad)
        {
            ProductoId = productoId;
            Nombre = nombre;
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
        }

        public decimal Subtotal
        {
            get { return PrecioUnitario * Cantidad; }
        }

        public override string ToString()
        {
            return $"{Cantidad} x {Nombre} @ {Formato.Dinero(PrecioUnitario)} = {Formato.Dinero(Subtotal)}";
        }
    }
}