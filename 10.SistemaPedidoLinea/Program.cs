using System;
using System.Collections.Generic;
using System.Globalization;

namespace Proyecto10_TiendaEnLinea
{
    internal class Program
    {
        static TiendaEnLinea tienda = new TiendaEnLinea();

        static void Main()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("=== TIENDA EN LÍNEA - GESTIÓN DE PEDIDOS ===");
                Console.WriteLine(" 1. Registrar producto");
                Console.WriteLine(" 2. Buscar producto");
                Console.WriteLine(" 3. Eliminar producto");
                Console.WriteLine(" 4. Registrar pedido");
                Console.WriteLine(" 5. Cancelar pedido");
                Console.WriteLine(" 6. Procesar pedido (siguiente de la cola normal)");
                Console.WriteLine(" 7. Atender pedido urgente");
                Console.WriteLine(" 8. Mostrar pedidos pendientes");
                Console.WriteLine(" 9. Mostrar historial");
                Console.WriteLine("10. Consultar inventario");
                Console.WriteLine("11. Buscar cliente");
                Console.WriteLine("12. Mostrar estadísticas");
                Console.WriteLine("13. Cargar datos de ejemplo");
                Console.WriteLine(" 0. Salir");

                opcion = LeerEntero("Seleccione una opción: ");
                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        RegistrarProducto();
                        break;
                    case 2:
                        BuscarProducto();
                        break;
                    case 3:
                        tienda.EliminarProducto(LeerEntero("ID del producto a eliminar: "));
                        Pausa();
                        break;
                    case 4:
                        RegistrarPedido();
                        break;
                    case 5:
                        tienda.CancelarPedido(LeerEntero("Número de pedido (ej. 1 para PED-0001): "));
                        Pausa();
                        break;
                    case 6:
                        tienda.ProcesarSiguiente();
                        Pausa();
                        break;
                    case 7:
                        tienda.AtenderUrgente();
                        Pausa();
                        break;
                    case 8:
                        tienda.MostrarPendientes();
                        Pausa();
                        break;
                    case 9:
                        tienda.MostrarHistorial();
                        Pausa();
                        break;
                    case 10:
                        Console.Write("Categoría (Enter para ver todo el inventario): ");
                        tienda.ConsultarInventario(Console.ReadLine());
                        Pausa();
                        break;
                    case 11:
                        tienda.BuscarCliente(LeerTexto("Nombre del cliente (o parte): "));
                        Pausa();
                        break;
                    case 12:
                        tienda.MostrarEstadisticas();
                        Pausa();
                        break;
                    case 13:
                        tienda.CargarDatosEjemplo();
                        Pausa();
                        break;
                    case 0:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Pausa();
                        break;
                }
            } while (opcion != 0);
        }


        static void Pausa()
        {
            Console.WriteLine();
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }

        static int LeerEntero(string mensaje)
        {
            int valor;
            Console.Write(mensaje);

            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Dato no válido, intenta de nuevo: ");
            }

            return valor;
        }

        static int LeerEnteroEnRango(string mensaje, int min, int max)
        {
            int valor = LeerEntero(mensaje);

            while (valor < min || valor > max)
            {
                valor = LeerEntero($"Debe estar entre {min} y {max}: ");
            }

            return valor;
        }

        static string LeerTexto(string mensaje)
        {
            string texto;

            do
            {
                Console.Write(mensaje);
                texto = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(texto));

            return texto.Trim();
        }

        static decimal LeerPrecio(string mensaje)
        {
            decimal valor;
            Console.Write(mensaje);

            while (!decimal.TryParse(Console.ReadLine()?.Trim(), NumberStyles.AllowDecimalPoint,
                                     CultureInfo.InvariantCulture, out valor) || valor <= 0)
            {
                Console.Write("Precio no válido (números con punto decimal, mayor a 0): ");
            }

            return valor;
        }


        static void RegistrarProducto()
        {
            string nombre = LeerTexto("Nombre del producto: ");
            string categoria = LeerTexto("Categoría: ");
            decimal precio = LeerPrecio("Precio: ");
            int existencias = LeerEnteroEnRango("Existencias: ", 0, 100000);

            Producto producto = tienda.RegistrarProducto(nombre, categoria, precio, existencias);

            Console.WriteLine($"Producto registrado con ID {producto.Id}.");
            Pausa();
        }

        static void BuscarProducto()
        {
            Console.WriteLine("Buscar por:");
            Console.WriteLine("1. ID");
            Console.WriteLine("2. Nombre");
            Console.WriteLine("3. Categoría");
            int tipo = LeerEntero("Opción: ");
            Console.WriteLine();

            List<Producto> encontrados = new List<Producto>();

            if (tipo == 1)
            {
                Producto producto = tienda.BuscarProductoPorId(LeerEntero("ID: "));

                if (producto != null)
                    encontrados.Add(producto);
            }
            else if (tipo == 2)
            {
                encontrados = tienda.BuscarProductosPorNombre(LeerTexto("Nombre (o parte): "));
            }
            else if (tipo == 3)
            {
                encontrados = tienda.BuscarProductosPorCategoria(LeerTexto("Categoría (o parte): "));
            }
            else
            {
                Console.WriteLine("Opción no válida.");
                Pausa();
                return;
            }

            Console.WriteLine();

            if (encontrados.Count == 0)
            {
                Console.WriteLine("No se encontraron productos.");
            }
            else
            {
                Console.WriteLine(Producto.Encabezado());
                Console.WriteLine(new string('-', 68));

                foreach (Producto producto in encontrados)
                {
                    Console.WriteLine(producto);
                }
            }

            Pausa();
        }

        static void RegistrarPedido()
        {
            if (tienda.CantidadProductos() == 0)
            {
                Console.WriteLine("Primero registra productos (opción 1 o 13).");
                Pausa();
                return;
            }

            string cliente = LeerTexto("Nombre del cliente: ");

            Console.WriteLine("Prioridad: 1. Normal   2. Alta   3. Urgente");
            int prioridad = LeerEnteroEnRango("Prioridad (1-3): ", 1, 3);

            List<ItemPedido> items = new List<ItemPedido>();

            while (true)
            {
                Console.WriteLine();
                int id = LeerEntero("ID del producto a agregar (0 para terminar): ");

                if (id == 0)
                    break;

                Producto producto = tienda.BuscarProductoPorId(id);

                if (producto == null)
                {
                    Console.WriteLine("No existe un producto con ese ID.");
                    continue;
                }

                // Si ya lo agregó antes, se suma a la misma línea
                ItemPedido existente = items.Find(i => i.ProductoId == id);
                int yaPedido = existente == null ? 0 : existente.Cantidad;
                int disponible = producto.Existencias - yaPedido;

                if (disponible <= 0)
                {
                    Console.WriteLine($"No hay más existencias de {producto.Nombre}.");
                    continue;
                }

                Console.WriteLine($"{producto.Nombre} - {Formato.Dinero(producto.Precio)} - disponibles: {disponible}");
                int cantidad = LeerEnteroEnRango("Cantidad: ", 1, disponible);

                if (existente != null)
                    existente.Cantidad += cantidad;
                else
                    items.Add(new ItemPedido(producto.Id, producto.Nombre, producto.Precio, cantidad));

                Console.WriteLine("Producto agregado al pedido.");
            }

            if (items.Count == 0)
            {
                Console.WriteLine("Pedido no registrado: no se agregó ningún producto.");
                Pausa();
                return;
            }

            Pedido pedido = tienda.RegistrarPedido(cliente, prioridad, items);

            if (pedido != null)
            {
                Console.WriteLine();
                pedido.MostrarDetalle();
                Console.WriteLine();

                if (pedido.EsUrgente)
                    Console.WriteLine("El pedido entró a la cola de pedidos urgentes (cola de prioridad).");
                else
                    Console.WriteLine("El pedido entró a la cola de pedidos pendientes.");
            }

            Pausa();
        }
    }
}