using System;

namespace Proyecto10_TiendaEnLinea
{
    public class Producto
    {
        public const int LimiteStockBajo = 5;

        public int Id { get; private set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Existencias { get; set; }
        public int Vendidas { get; set; } 

        public Producto(int id, string nombre, string categoria, decimal precio, int existencias)
        {
            Id = id;
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
            Existencias = existencias;
            Vendidas = 0;
        }

        public bool EsStockBajo
        {
            get { return Existencias <= LimiteStockBajo; }
        }

        public static string Encabezado()
        {
            return "ID".PadRight(6) +
                   "Nombre".PadRight(22) +
                   "Categoría".PadRight(16) +
                   "Precio".PadRight(13) +
                   "Existencias";
        }

        public override string ToString()
        {
            string nombre = Nombre.Length > 20 ? Nombre.Substring(0, 20) : Nombre;
            string categoria = Categoria.Length > 14 ? Categoria.Substring(0, 14) : Categoria;

            return Id.ToString().PadRight(6) +
                   nombre.PadRight(22) +
                   categoria.PadRight(16) +
                   Formato.Dinero(Precio).PadRight(13) +
                   Existencias.ToString() +
                   (EsStockBajo ? "  (stock bajo)" : "");
        }
    }
}