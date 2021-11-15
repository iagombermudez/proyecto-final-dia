using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BusquedasUI.Core.Base;

namespace BusquedasUI.Core.Xml
{
    public class ClienteXmlReader
    {
        public List<Cliente> GetClientes()
        {
            XElement raiz = XmlReader.GetRootNode("Clientes.xml");
            IEnumerable<XElement> clientes = XmlReader.GetElements(raiz, "Cliente");

            return clientes.Select(GetCliente).ToList();
        }

        private Cliente GetCliente(XElement clienteXml)
        {
            var nombre = clienteXml.Element("Nombre")?.Value;
            
            var cliente = new Cliente(nombre);
            return cliente;
        }
    }
}