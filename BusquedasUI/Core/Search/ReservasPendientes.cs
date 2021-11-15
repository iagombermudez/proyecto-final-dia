using System;
using System.Collections.Generic;
using BusquedasUI.Core.Base;
using BusquedasUI.Core.Xml;

namespace BusquedasUI.Core.Search
{
    public static class ReservasPendientes
    {
        public static List<Reserva> GetReservasUltimosDias( int dias)
        {
            List<Reserva> toret = new List<Reserva>();
            
            DateTime today = DateTime.Today;
            DateTime dateLimit = today.AddDays(dias);


            List<Reserva> reservasSinFiltrar = GetReservas();
            
            foreach (var reserva in reservasSinFiltrar)
            {
                DateTime fechaEntrada = Convert.ToDateTime(reserva.FechaEntrada);
                if (fechaEntrada >= today && fechaEntrada <= dateLimit)
                {
                    toret.Add(reserva);
                }
            }
            return toret;
        }

        private static List<Reserva> GetReservas()
        {
            ReservaXmlReader xmlReader = new ReservaXmlReader();
            return xmlReader.GetReservas();
        }
    }
}