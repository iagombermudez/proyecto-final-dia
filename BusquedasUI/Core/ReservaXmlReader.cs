using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace BusquedasUI.Core
{
    public class ReservaXmlReader:XmlReader
    {

        public List<Reserva> GetReservas()
        {
            List<Reserva> toret = new List<Reserva>();
            
            XElement raiz = GetRootNode("Reservas.xml");
            IEnumerable<XElement> reservas = GetElements(raiz, "Reserva");

            foreach (XElement reservaXml in reservas)
            {

                Reserva reserva = GetReserva(reservaXml);
                toret.Add(reserva);
                Console.WriteLine(reserva.ToString());

            }

            return toret;
        }

        private Reserva GetReserva(XElement reservaXml)
        {
            var id = reservaXml.Element("Id")?.Value;
            var tipo = reservaXml.Element("Tipo")?.Value;
            var cliente = reservaXml.Element("Cliente")?.Value;
            var fechaEntrada = reservaXml.Element("FechaEntrada")?.Value;
            var fechaSalida = reservaXml.Element("FechaSalida")?.Value;
            var garaje = reservaXml.Element("Garaje")?.Value;
            var garajeValue = CalculateGarajeValue(garaje);
            var importe = Convert.ToDouble(reservaXml.Element("Importe")?.Value);
            var iva = Convert.ToDouble(reservaXml.Element("Iva")?.Value);

            var reserva = new Reserva(id, tipo, cliente,
                fechaEntrada, fechaSalida, garajeValue, importe, iva);

            return reserva;
        }

        private bool CalculateGarajeValue(string garaje)
        {
            return garaje.ToUpper() == "SI" ? true : false;
        }
    }
}