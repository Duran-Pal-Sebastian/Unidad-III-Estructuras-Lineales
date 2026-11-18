using System;

namespace Proyecto3_Simular_Llamadas
{
    public class Llamada
    {
        public string Contacto { get; set; }
        public string Numero { get; set; }
        public DateTime HoraInicio { get; set; }

        public Llamada(string contacto, string numero)
        {
            Contacto = contacto;
            Numero = numero;
            HoraInicio = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Contacto} ({Numero}) - inició a las {HoraInicio:HH:mm:ss}";
        }
    }
}