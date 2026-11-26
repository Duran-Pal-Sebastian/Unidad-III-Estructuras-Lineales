using System;
using System.Collections.Generic;

namespace Proyecto9_ControlProcesos
{
    
    public class GestorProcesos
    {
        private const int CapacidadListos = 10;

        private ListaProcesos tabla = new ListaProcesos();
        private ColaCircular listos = new ColaCircular(CapacidadListos);
        private Cola bloqueados = new Cola();
        private int siguientePid = 1;

        public int Quantum { get; set; } = 3;

        // ---------- Crear ----------
        public Proceso Crear(string nombre, int prioridad, int tiempo)
        {
            Proceso proceso = new Proceso(siguientePid, nombre, prioridad, tiempo);
            siguientePid++;
            tabla.Agregar(proceso);
            return proceso;
        }

        // ---------- NUEVO -> LISTO ----------
        public bool Admitir(Proceso proceso)
        {
            if (proceso.Estado != EstadoProceso.Nuevo)
            {
                Console.WriteLine($"{proceso.Etiqueta} no está NUEVO (estado actual: {proceso.EstadoTexto}).");
                return false;
            }

            if (listos.Llena())
            {
                Console.WriteLine($"La cola de listos está llena ({listos.Capacidad}). {proceso.Etiqueta} sigue NUEVO.");
                return false;
            }

            proceso.Estado = EstadoProceso.Listo;
            listos.Encolar(proceso);
            Console.WriteLine($"{proceso.Etiqueta} admitido: NUEVO -> LISTO.");
            return true;
        }

        public void AdmitirNuevos()
        {
            List<Proceso> nuevos = tabla.ObtenerPorEstado(EstadoProceso.Nuevo);

            if (nuevos.Count == 0)
            {
                Console.WriteLine("No hay procesos nuevos por admitir.");
                return;
            }

            foreach (Proceso proceso in nuevos)
            {
                if (!Admitir(proceso))
                    break;
            }
        }

        // ---------- Ejecutar (una ráfaga de CPU = un quantum) ----------
        public void EjecutarSiguiente()
        {
            Proceso proceso = listos.Desencolar();

            if (proceso == null)
            {
                Console.WriteLine("No hay procesos listos para ejecutar.");
                return;
            }

            proceso.Estado = EstadoProceso.Ejecutando;
            int uso = Math.Min(Quantum, proceso.TiempoRestante);
            proceso.TiempoRestante -= uso;

            Console.WriteLine($"{proceso.Etiqueta} ({proceso.Nombre}): LISTO -> EJECUTANDO " +
                              $"| usa {uso} u. de CPU | restante: {proceso.TiempoRestante}");

            if (proceso.TiempoRestante == 0)
            {
                proceso.Estado = EstadoProceso.Terminado;
                Console.WriteLine($"{proceso.Etiqueta} terminó: EJECUTANDO -> TERMINADO.");
            }
            else
            {
                proceso.Estado = EstadoProceso.Listo;
                listos.Encolar(proceso);
                Console.WriteLine($"{proceso.Etiqueta} agotó su quantum: EJECUTANDO -> LISTO (regresa al final de la cola).");
            }
        }

        public void EjecutarTodos()
        {
            if (listos.Vacia())
            {
                Console.WriteLine("No hay procesos listos para ejecutar.");
                return;
            }

            while (!listos.Vacia())
            {
                EjecutarSiguiente();
                Console.WriteLine();
            }

            Console.WriteLine("La cola de listos quedó vacía.");
        }

        // ---------- Bloquear / Desbloquear ----------
        public void Bloquear(int pid)
        {
            Proceso proceso = tabla.Buscar(pid);

            if (proceso == null)
            {
                Console.WriteLine("No existe un proceso con ese PID.");
                return;
            }

            if (proceso.Estado != EstadoProceso.Listo)
            {
                Console.WriteLine($"Solo se puede bloquear un proceso LISTO. {proceso.Etiqueta} está {proceso.EstadoTexto}.");
                return;
            }

            listos.Remover(proceso);
            proceso.Estado = EstadoProceso.Bloqueado;
            bloqueados.Encolar(proceso);

            Console.WriteLine($"{proceso.Etiqueta} bloqueado: LISTO -> BLOQUEADO.");
        }

        public void Desbloquear(int pid)
        {
            Proceso proceso = tabla.Buscar(pid);

            if (proceso == null)
            {
                Console.WriteLine("No existe un proceso con ese PID.");
                return;
            }

            if (proceso.Estado != EstadoProceso.Bloqueado)
            {
                Console.WriteLine($"{proceso.Etiqueta} no está BLOQUEADO (estado actual: {proceso.EstadoTexto}).");
                return;
            }

            if (listos.Llena())
            {
                Console.WriteLine($"La cola de listos está llena ({listos.Capacidad}). {proceso.Etiqueta} sigue BLOQUEADO.");
                return;
            }

            bloqueados.Remover(proceso);
            proceso.Estado = EstadoProceso.Listo;
            listos.Encolar(proceso);

            Console.WriteLine($"{proceso.Etiqueta} desbloqueado: BLOQUEADO -> LISTO.");
        }

        // ---------- Eliminar ----------
        public void Eliminar(int pid)
        {
            Proceso proceso = tabla.Buscar(pid);

            if (proceso == null)
            {
                Console.WriteLine("No existe un proceso con ese PID.");
                return;
            }

            // Primero se saca de la cola donde esté, luego de la tabla.
            if (proceso.Estado == EstadoProceso.Listo)
                listos.Remover(proceso);
            else if (proceso.Estado == EstadoProceso.Bloqueado)
                bloqueados.Remover(proceso);

            tabla.Eliminar(pid);

            Console.WriteLine($"{proceso.Etiqueta} ({proceso.Nombre}) eliminado del sistema.");
        }

        // ---------- Buscar ----------
        public Proceso BuscarPorPid(int pid)
        {
            return tabla.Buscar(pid);
        }

        public List<Proceso> BuscarPorNombre(string nombre)
        {
            return tabla.BuscarPorNombre(nombre);
        }

        // ---------- Mostrar ----------
        public void MostrarProcesos()
        {
            tabla.Mostrar();
        }

        public void MostrarColas()
        {
            Console.WriteLine($"COLA DE LISTOS (circular {listos.Contar()}/{listos.Capacidad}) | Quantum: {Quantum}");
            listos.Mostrar();

            Console.WriteLine();
            Console.WriteLine($"COLA DE BLOQUEADOS ({bloqueados.Contar()})");
            bloqueados.Mostrar();
        }
    }
}