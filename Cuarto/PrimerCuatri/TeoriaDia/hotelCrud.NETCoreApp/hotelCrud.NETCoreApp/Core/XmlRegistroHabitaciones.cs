using System;
using System.Xml.Linq;

namespace hotel.IO
{
    public class XmlRegistroHabitaciones
    {
        private static string TAG_ROOT="habitaciones";
        private static string TAG_HABITACION="habitacion";
        private static string TAG_ID="id";
        private static string TAG_TIPO="tipo";
        private static string TAG_FECHA_RENOVACION="fecha_renovacion";
        private static string TAG_FECHA_RESERVA="fecha_reserva";
        private static string TAG_WIFI="wifi";
        private static string TAG_CAJAFUERTE="cajaFuerte";
        private static string TAG_MINIBAR="miniBar";
        private static string TAG_BANO = "bano";
        private static string TAG_COCINA = "cocina";
        private static string TAG_TV = "tv";
        private static string DEFAULT_FILE_HABITACIONES = "habitaciones.xml";

        private RegistroHabitaciones h;
        public XmlRegistroHabitaciones(RegistroHabitaciones x)
        {
            this.h = x;
        }

        public static RegistroHabitaciones RecuperarXML(string nf)
        {
            XDocument doc=XDocument.Load(nf);
            return cargarXML(doc.Root);
        }
        public static RegistroHabitaciones RecuperarXML()
        {
            XDocument doc=XDocument.Load(XmlRegistroHabitaciones.DEFAULT_FILE_HABITACIONES);
            return cargarXML(doc.Root);
        }

        private static RegistroHabitaciones cargarXML(XElement root)
        {
            string rootTag = root?.Name.ToString() ?? "";
            RegistroHabitaciones toret = new RegistroHabitaciones();
            
            if (root != null && rootTag == XmlRegistroHabitaciones.TAG_ROOT)
            {
                var elems = root.Elements(TAG_HABITACION);
                Habitacion toAdd;
                foreach (var habitacion in elems)
                {
                    int id = Int32.Parse(habitacion.Element(TAG_ID).Value);
                    string tipo = habitacion.Element(TAG_TIPO).Value;
                    DateTime fechaRenovacion=Convert.ToDateTime(habitacion.Element(TAG_FECHA_RENOVACION).Value);
                    DateTime fechaReserva=Convert.ToDateTime(habitacion.Element(TAG_FECHA_RESERVA).Value);
                    bool wifi = Boolean.Parse(habitacion.Element(TAG_WIFI).Value);
                    bool cajaFuerte = Boolean.Parse(habitacion.Element(TAG_CAJAFUERTE).Value);
                    bool miniBar = Boolean.Parse(habitacion.Element(TAG_MINIBAR).Value);
                    bool bano = Boolean.Parse(habitacion.Element(TAG_BANO).Value);
                    bool cocina = Boolean.Parse(habitacion.Element(TAG_COCINA).Value);
                    bool tv = Boolean.Parse(habitacion.Element(TAG_TV).Value);

                    toAdd = new Habitacion(id, tipo,fechaRenovacion, fechaReserva,wifi,cajaFuerte,miniBar,bano, cocina, tv);
                    toret.Add(toAdd);
                }
            }

            return toret;
        }

        public void Guardar(string nf)
        {
            var doc = new XDocument();
            doc.Add(this.ToXML());
            doc.Save(nf);
        }
        public XElement ToXML()
        {
            XElement toret=new XElement(TAG_ROOT);
            
            foreach(var h in this.h)
            {
                XElement estancia = new XElement(TAG_HABITACION);
                
                XElement id = new XElement(TAG_ID,h.Id);
                estancia.Add(id);
                XElement type = new XElement(TAG_TIPO,h.Tipo);
                estancia.Add(type);
                XElement fechaRenovacion = new XElement(TAG_FECHA_RENOVACION,h.FechaRenovacion);
                estancia.Add(fechaRenovacion);
                XElement fechaReserva = new XElement(TAG_FECHA_RESERVA,h.FechaReserva);
                estancia.Add(fechaReserva);
                XElement wifi = new XElement(TAG_WIFI,h.Wifi);
                estancia.Add(wifi);
                XElement cajaFuerte = new XElement(TAG_CAJAFUERTE, h.CajaFuerte);
                estancia.Add(cajaFuerte);
                XElement miniBar = new XElement(TAG_MINIBAR,h.MiniBar);
                estancia.Add(miniBar);
                XElement bano = new XElement(TAG_BANO, h.Bano);
                estancia.Add(bano);
                XElement cocina = new XElement(TAG_COCINA, h.Cocina);
                estancia.Add(cocina);
                XElement tv = new XElement(TAG_TV, h.Tv);
                estancia.Add(tv);
                
                toret.Add(estancia);
            }
            return toret;
        }
    }
}