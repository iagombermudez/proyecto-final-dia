using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusquedasUI.Core
{
    public static class ReservasClientes
    {
        public static List<Reserva> GetReservas()
        {
            ReservaXmlReader xmlReader = new ReservaXmlReader();
            List<Reserva> reservasClientes = xmlReader.GetReservas();

            reservasClientes = reservasClientes.OrderBy(x => x.Cliente).ToList();
            
            return reservasClientes;
        }
        
        public static List<Reserva> GetReservasPerYear(int year)
        {
            ReservaXmlReader xmlReader = new ReservaXmlReader();
            List<Reserva> reservasClientes = xmlReader.GetReservas();

            //filtrar reservas por año
            //la forma de hacerlo no esta "bien"
            //cambiar para el proyecto final
            reservasClientes = reservasClientes.
                Where(x => x.FechaEntrada.Substring(x.FechaEntrada.Length - 4) == year.ToString()).
                OrderBy(x => x.Cliente)
                .ToList();
            
            return reservasClientes;
        }
    }
}