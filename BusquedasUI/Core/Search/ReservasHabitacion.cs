using System.Collections.Generic;
using System.Linq;
using BusquedasUI.Core.Base;
using BusquedasUI.Core.Xml;

namespace BusquedasUI.Core.Search
{
    public static class ReservasHabitacion
    {
        public static List<Reserva> GetReservas()
        {
            ReservaXmlReader xmlReader = new ReservaXmlReader();
            List<Reserva> reservasClientes = xmlReader.GetReservas();

            reservasClientes = reservasClientes.OrderBy(x => x.getNumHabitacion()).ToList();
            
            return reservasClientes;
        }
        
        public static List<Reserva> GetReservasPerYear(int year)
        {
            List<Reserva> reservasClientes = GetReservas();

            //filtrar reservas por año
            //la forma de hacerlo no esta "bien"
            //cambiar para el proyecto final
            reservasClientes = reservasClientes.
                Where(x => x.FechaEntrada.Substring(x.FechaEntrada.Length - 4) == year.ToString())
                .ToList();
            
            return reservasClientes;
        }
    }
}