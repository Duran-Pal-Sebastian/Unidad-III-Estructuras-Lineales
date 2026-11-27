using System;
using System.Collections.Generic;

namespace Proyecto10_TiendaEnLinea
{
    public class TiendaEnLinea
    {
        private ListaProductos productos = new ListaProductos();
        private ListaPedidos pedidos = new ListaPedidos();
        private ColaPedidos pendientes = new ColaPedidos();
        private ColaPrioridad urgentes = new ColaPrioridad();
        private PilaHistorial historial = new PilaHistorial();

        private int siguienteIdProducto = 1;
        private int siguienteNumeroPedido = 1;

        private void Registrar(string descripcion)
        {
            historial.Apilar(new Operacion(descripcion));
        }


        public int CantidadProductos()
        {
            return productos.Contar();
        }

        public Producto RegistrarProducto(string nombre, string categoria, decimal precio, int existencias)
        {
            Producto producto = new Producto(siguienteIdProducto, nombre, categoria, precio, existencias);
            siguienteIdProducto++;
            productos.Agregar(producto);

            Registrar($"Producto registrado: #{producto.Id} {nombre} ({categoria}), " +
                      $"{Formato.Dinero(precio)}, existencias: {existencias}");
            return producto;
        }

        public Producto BuscarProductoPorId(int id)
        {
            return productos.Buscar(id);
        }

        public List<Producto> BuscarProductosPorNombre(string texto)
        {
            return productos.BuscarPorNombre(texto);
        }

        public List<Producto> BuscarProductosPorCategoria(string texto)
        {
            return productos.BuscarPorCategoria(texto);
        }

        public void EliminarProducto(int id)
        {
            Producto producto = productos.Buscar(id);

            if (producto == null)
            {
                Console.WriteLine("No existe un producto con ese ID.");
                return;
            }

            if (pedidos.ProductoEnPedidosPendientes(id))
            {
                Console.WriteLine($"No se puede eliminar {producto.Nombre}: está en pedidos pendientes. " +
                                  "Procésalos o cancélalos primero.");
                return;
            }

            productos.Eliminar(id);
            Registrar($"Producto eliminado: #{producto.Id} {producto.Nombre}");
            Console.WriteLine($"Producto #{producto.Id} ({producto.Nombre}) eliminado.");
        }

        public void ConsultarInventario(string categoria)
        {
            int mostrados = productos.Mostrar(categoria);

            if (mostrados == 0)
            {
                if (string.IsNullOrWhiteSpace(categoria))
                    Console.WriteLine("El inventario está vacío.");
                else
                    Console.WriteLine("No hay productos en esa categoría.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"Productos mostrados: {mostrados}");
            }
        }


        public Pedido RegistrarPedido(string cliente, int prioridad, List<ItemPedido> items)
        {
            if (items.Count == 0)
            {
                Console.WriteLine("El pedido no tiene productos.");
                return null;
            }

            foreach (ItemPedido item in items)
            {
                Producto producto = productos.Buscar(item.ProductoId);

                if (producto == null || item.Cantidad <= 0 || item.Cantidad > producto.Existencias)
                {
                    Console.WriteLine($"No se puede surtir {item.Nombre}: existencias insuficientes o producto inexistente.");
                    return null;
                }
            }

            foreach (ItemPedido item in items)
            {
                productos.Buscar(item.ProductoId).Existencias -= item.Cantidad;
            }

            Pedido pedido = new Pedido(siguienteNumeroPedido, cliente, prioridad, items);
            siguienteNumeroPedido++;

            pedidos.Agregar(pedido);

            if (pedido.EsUrgente)
                urgentes.Encolar(pedido);
            else
                pendientes.Encolar(pedido);

            Registrar($"Pedido {pedido.Etiqueta} registrado: cliente {cliente}, " +
                      $"total {Formato.Dinero(pedido.Total)}, prioridad {pedido.PrioridadTexto}");
            return pedido;
        }

        public void CancelarPedido(int numero)
        {
            Pedido pedido = pedidos.Buscar(numero);

            if (pedido == null)
            {
                Console.WriteLine("No existe un pedido con ese número.");
                return;
            }

            if (pedido.Estado != EstadoPedido.Pendiente)
            {
                Console.WriteLine($"Solo se puede cancelar un pedido PENDIENTE. {pedido.Etiqueta} está {pedido.EstadoTexto}.");
                return;
            }

            if (pedido.EsUrgente)
                urgentes.Remover(pedido);
            else
                pendientes.Remover(pedido);

            foreach (ItemPedido item in pedido.Productos)
            {
                Producto producto = productos.Buscar(item.ProductoId);

                if (producto != null)
                    producto.Existencias += item.Cantidad;
            }

            pedido.Estado = EstadoPedido.Cancelado;
            Registrar($"Pedido {pedido.Etiqueta} cancelado (cliente {pedido.Cliente}); existencias devueltas");
            Console.WriteLine($"{pedido.Etiqueta} cancelado. Las existencias regresaron al inventario.");
        }

        public void ProcesarSiguiente()
        {
            Pedido pedido = pendientes.Desencolar();

            if (pedido == null)
            {
                Console.WriteLine("No hay pedidos pendientes normales.");

                if (!urgentes.Vacia())
                    Console.WriteLine($"Hay {urgentes.Contar()} pedido(s) urgente(s): usa 'Atender pedido urgente'.");

                return;
            }

            Completar(pedido, "procesado");
        }

        public void AtenderUrgente()
        {
            Pedido pedido = urgentes.Desencolar();

            if (pedido == null)
            {
                Console.WriteLine("No hay pedidos urgentes.");
                return;
            }

            Completar(pedido, "atendido (URGENTE)");
        }

        private void Completar(Pedido pedido, string accion)
        {
            pedido.Estado = EstadoPedido.Procesado;

            foreach (ItemPedido item in pedido.Productos)
            {
                Producto producto = productos.Buscar(item.ProductoId);

                if (producto != null)
                    producto.Vendidas += item.Cantidad;
            }

            Registrar($"Pedido {pedido.Etiqueta} {accion}: cliente {pedido.Cliente}, total {Formato.Dinero(pedido.Total)}");

            Console.WriteLine($"Pedido {accion}:");
            Console.WriteLine();
            pedido.MostrarDetalle();
        }

        public void MostrarPendientes()
        {
            Console.WriteLine($"=== PEDIDOS URGENTES (cola de prioridad): {urgentes.Contar()} ===");
            urgentes.Mostrar();

            Console.WriteLine();
            Console.WriteLine($"=== PEDIDOS PENDIENTES (cola normal): {pendientes.Contar()} ===");
            pendientes.Mostrar();
        }

        public void MostrarHistorial()
        {
            Console.WriteLine($"=== HISTORIAL DE OPERACIONES ({historial.Contar()}) - más reciente primero ===");
            historial.Mostrar(20);
        }

        public void BuscarCliente(string texto)
        {
            List<Pedido> encontrados = pedidos.BuscarPorCliente(texto);

            if (encontrados.Count == 0)
            {
                Console.WriteLine("No se encontraron pedidos de ese cliente.");
                return;
            }

            Console.WriteLine(Pedido.Encabezado());
            Console.WriteLine(new string('-', 78));

            decimal total = 0;
            foreach (Pedido pedido in encontrados)
            {
                Console.WriteLine(pedido);

                if (pedido.Estado != EstadoPedido.Cancelado)
                    total += pedido.Total;
            }

            Console.WriteLine();
            Console.WriteLine($"Pedidos encontrados: {encontrados.Count} | Suma (sin cancelados): {Formato.Dinero(total)}");
        }


        public void MostrarEstadisticas()
        {
            List<Producto> listaProductos = productos.ObtenerTodos();
            List<Pedido> listaPedidos = pedidos.ObtenerTodos();

            int unidades = 0;
            decimal valorInventario = 0;
            int stockBajo = 0;
            Producto masVendido = null;

            foreach (Producto producto in listaProductos)
            {
                unidades += producto.Existencias;
                valorInventario += producto.Precio * producto.Existencias;

                if (producto.EsStockBajo)
                    stockBajo++;

                if (producto.Vendidas > 0 &&
                    (masVendido == null || producto.Vendidas > masVendido.Vendidas))
                {
                    masVendido = producto;
                }
            }

            int procesados = 0;
            int cancelados = 0;
            decimal ingresos = 0;
            Dictionary<string, decimal> gastoPorCliente =
                new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

            foreach (Pedido pedido in listaPedidos)
            {
                if (pedido.Estado == EstadoPedido.Procesado)
                {
                    procesados++;
                    ingresos += pedido.Total;

                    if (!gastoPorCliente.ContainsKey(pedido.Cliente))
                        gastoPorCliente[pedido.Cliente] = 0;

                    gastoPorCliente[pedido.Cliente] += pedido.Total;
                }
                else if (pedido.Estado == EstadoPedido.Cancelado)
                {
                    cancelados++;
                }
            }

            string mejorCliente = null;
            decimal mejorGasto = 0;

            foreach (KeyValuePair<string, decimal> par in gastoPorCliente)
            {
                if (par.Value > mejorGasto)
                {
                    mejorGasto = par.Value;
                    mejorCliente = par.Key;
                }
            }

            Console.WriteLine("=== ESTADÍSTICAS ===");
            Console.WriteLine();
            Console.WriteLine("INVENTARIO");
            Console.WriteLine($"  Productos registrados:   {listaProductos.Count}");
            Console.WriteLine($"  Unidades en existencia:  {unidades}");
            Console.WriteLine($"  Valor del inventario:    {Formato.Dinero(valorInventario)}");
            Console.WriteLine($"  Productos con stock bajo (<= {Producto.LimiteStockBajo}): {stockBajo}");
            Console.WriteLine();
            Console.WriteLine("PEDIDOS");
            Console.WriteLine($"  Registrados:             {listaPedidos.Count}");
            Console.WriteLine($"  Pendientes normales:     {pendientes.Contar()}");
            Console.WriteLine($"  Pendientes urgentes:     {urgentes.Contar()}");
            Console.WriteLine($"  Procesados:              {procesados}");
            Console.WriteLine($"  Cancelados:              {cancelados}");
            Console.WriteLine();
            Console.WriteLine("VENTAS");
            Console.WriteLine($"  Ingresos (procesados):   {Formato.Dinero(ingresos)}");

            if (procesados > 0)
                Console.WriteLine($"  Ticket promedio:         {Formato.Dinero(ingresos / procesados)}");
            else
                Console.WriteLine("  Ticket promedio:         (sin ventas todavía)");

            if (masVendido != null)
                Console.WriteLine($"  Producto más vendido:    {masVendido.Nombre} ({masVendido.Vendidas} unidades)");
            else
                Console.WriteLine("  Producto más vendido:    (sin ventas todavía)");

            if (mejorCliente != null)
                Console.WriteLine($"  Mejor cliente:           {mejorCliente} ({Formato.Dinero(mejorGasto)})");
            else
                Console.WriteLine("  Mejor cliente:           (sin ventas todavía)");
        }


        private ItemPedido Item(int productoId, int cantidad)
        {
            Producto p = productos.Buscar(productoId);
            return new ItemPedido(p.Id, p.Nombre, p.Precio, cantidad);
        }

        public void CargarDatosEjemplo()
        {
            if (productos.Contar() > 0 || pedidos.Contar() > 0)
            {
                Console.WriteLine("Ya hay datos registrados: los datos de ejemplo solo se cargan con el sistema vacío.");
                return;
            }

            RegistrarProducto("Laptop Lenovo", "Computo", 12500m, 8);
            RegistrarProducto("Mouse inalámbrico", "Accesorios", 250m, 30);
            RegistrarProducto("Teclado mecánico", "Accesorios", 899.90m, 12);
            RegistrarProducto("Monitor 24 pulgadas", "Computo", 3200m, 6);
            RegistrarProducto("Audífonos Bluetooth", "Audio", 750m, 4);
            RegistrarProducto("Bocina portátil", "Audio", 1100m, 15);

            RegistrarPedido("Ana López", 1, new List<ItemPedido> { Item(2, 2), Item(3, 1) });
            RegistrarPedido("Luis Pérez", 3, new List<ItemPedido> { Item(1, 1) });
            RegistrarPedido("Marta Ruiz", 2, new List<ItemPedido> { Item(5, 2), Item(6, 1) });
            RegistrarPedido("Pedro Soto", 1, new List<ItemPedido> { Item(4, 1) });

            Console.WriteLine("Datos de ejemplo cargados: 6 productos y 4 pedidos (2 normales y 2 urgentes).");
        }
    }
}