using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BusquedasUI.Core.Base;

namespace BusquedasUI.Core.Xml
{
    public class HabitacionXmlReader
    {

        public List<Habitacion> GetHabitaciones()
        {
            List<Habitacion> toret = new List<Habitacion>();

            XElement raiz = XmlReader.GetRootNode("Habitaciones.xml");
            IEnumerable<XElement> habitaciones = XmlReader.GetElements(raiz, "Habitacion");

            foreach (XElement habitacionXml in habitaciones)
            {

                Habitacion habitacion = GetHabitacion(habitacionXml);
                Console.WriteLine(habitacion.ToString());
                toret.Add(habitacion);
            }

            return toret;
        }

        private Habitacion GetHabitacion(XElement habitacionXml)
        {
            var id = habitacionXml.Element("Id")?.Value;
            var tipo = habitacionXml.Element("Tipo")?.Value;
            var fechaUltimaRenovacion = habitacionXml.Element("FechaUltimaRenovacion")?.Value;
            var fechaUltimaReserva = habitacionXml.Element("FechaUltimaReserva")?.Value;
            var comodidades = GetComodidades(habitacionXml);
            
            var habitacion = new Habitacion(id, tipo, fechaUltimaRenovacion,
                fechaUltimaReserva, comodidades);
            
            return habitacion;
        }

        private List<String> GetComodidades(XElement habitacionXml)
        {
            var comodidadesElement = habitacionXml.Element("Comodidades");
            var comodidadesElements = XmlReader.GetElements(comodidadesElement, "Comodidad");
            var comodidades = comodidadesElements.Select(comodidad => comodidad.Value).ToList();
            return comodidades;
        }
    }
}