using System;
using System.Collections.Generic;

namespace Proyecto10_TiendaEnLinea
{
    public class Pedido
    {
        public int Numero { get; private set; }
        public string Cliente { get; private set; }
        public DateTime Fecha { get; private set; }
        public List<ItemPedido> Productos { get; private set; }
        public int Prioridad { get; private set; }      
        public EstadoPedido Estado { get; set; }

        public Pedido(int numero, string cliente, int prioridad, List<ItemPedido> productos)
        {
            Numero = numero;
            Cliente = cliente;
            Fecha = DateTime.Now;
            Prioridad = prioridad;
            Productos = productos;
            Estado = EstadoPedido.Pendiente;
        }

        public decimal Total
        {
            get
            {
                decimal total = 0;
                foreach (ItemPedido item in Productos)
                {
                    total += item.Subtotal;
                }
                return total;
            }
        }

        public string Etiqueta
        {
            get { return "PED-" + Numero.ToString("0000"); }
        }

        public bool EsUrgente
        {
            get { return Prioridad >= 2; }
        }

        public string PrioridadTexto
        {
            get
            {
                if (Prioridad == 3) return "Urgente";
                if (Prioridad == 2) return "Alta";
                return "Normal";
            }
        }

        public string EstadoTexto
        {
            get { return Estado.ToString().ToUpperInvariant(); }
        }

        public static string Encabezado()
        {
            return "Pedido".PadRight(11) +
                   "Cliente".PadRight(17) +
                   "Fecha".PadRight(18) +
                   "Prioridad".PadRight(11) +
                   "Total".PadRight(13) +
                   "Estado";
        }

        public override string ToString()
        {
            string cliente = Cliente.Length > 15 ? Cliente.Substring(0, 15) : Cliente;

            return Etiqueta.PadRight(11) +
                   cliente.PadRight(17) +
                   Fecha.ToString("dd/MM/yyyy HH:mm").PadRight(18) +
                   PrioridadTexto.PadRight(11) +
                   Formato.Dinero(Total).PadRight(13) +
                   EstadoTexto;
        }

        public void MostrarDetalle()
        {
            Console.WriteLine($"Pedido:    {Etiqueta}");
            Console.WriteLine($"Cliente:   {Cliente}");
            Console.WriteLine($"Fecha:     {Fecha:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Prioridad: {PrioridadTexto}");
            Console.WriteLine($"Estado:    {EstadoTexto}");
            Console.WriteLine("Productos:");

            foreach (ItemPedido item in Productos)
            {
                Console.WriteLine("  " + item);
            }

            Console.WriteLine($"Total:     {Formato.Dinero(Total)}");
        }
    }
}