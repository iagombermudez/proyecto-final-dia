using System.IO;
using System.Xml.Linq;
using gestionHotel.core.coreClientes;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.coreReservas;
using gestionHotel.core.coreReservas.IO;
using hotel.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace gestionHotel.core.IO
{
    public class XmlGeneral
    {
        public static string TAG_ROOT = "info";

        private RegistroGeneral rg;
        
        public XmlGeneral(RegistroGeneral rg)
        {
            this.rg = rg;
        }

        public static RegistroGeneral cargarXML(string nf)
        {
            XDocument doc=XDocument.Load(nf);
            return cargarXML(doc.Root);
        }

        private static string FICHERO_INFO_DEFAULT = "infoGeneral.xml";

        public static RegistroGeneral cargarXML(XElement root)
        {
            RegistroGeneral toret=null;
            
            string rootTag=root?.Name.ToString() ?? "";

            if (root != null && rootTag == XmlGeneral.TAG_ROOT)
            {
                RegistroHabitaciones regH = XmlRegistroHabitaciones.cargarXML(root.Element(XmlRegistroHabitaciones.TAG_ROOT));
                //CARGAR CLIENTES
                //RegistroReservas regR = XmlRegistroReservas.cargarXML(root.Element(XmlRegistroReservas.TAG_ROOT));
                
                toret = new RegistroGeneral(regH);
            }
            
            return toret;
        }
        
        public void GuardarInfoGeneral(string nf)
        {
            var doc = new XDocument();
            doc.Add(this.ToXML());
            doc.Save(nf);
        }
        public XElement ToXML()
        {
            XElement toret=new XElement(TAG_ROOT);

            XmlRegistroHabitaciones xmlH = new XmlRegistroHabitaciones(this.rg.H);
            
            toret.Add(xmlH.ToXML());
            /*
            XmlRegistroClientes xmlC = new XmlRegistroClientes(this.rg.C);
            
            toret.Add(xmlH.ToXML());
            
            XmlRegistroReservas xmlR = new XmlRegistroReservas(this.rg.R);
            
            toret.Add(xmlR.ToXML());*/
                        
            return toret;
        }
    }
        
        
}
