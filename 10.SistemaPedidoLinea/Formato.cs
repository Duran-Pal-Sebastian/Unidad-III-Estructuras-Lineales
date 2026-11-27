using System;
using System.Globalization;

namespace Proyecto10_TiendaEnLinea
{
    public static class Formato
    {
        public static string Dinero(decimal valor)
        {
            return "$" + valor.ToString("N2", CultureInfo.InvariantCulture);
        }
    }
}