using System;
namespace AtencionBanco
{
    public class Cliente
    {
    public int turno { get; set; }
    public string Nombre { get; set; }
    public int prioridad { get; set; }
    
    public Cliente(int turno, string nombre, int prioridad)
        {
            this.turno = turno;
            this.Nombre = nombre;
            this.prioridad = prioridad;
        }
    public override string ToString()
        {
            return $"Turno:{turno} | Nombre:{Nombre} | Prioridad:{prioridad}";
        }

    }
