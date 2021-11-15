using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BusquedasUI.Core
{
    public class ClienteXmlReader:XmlReader
    {
        public List<Cliente> GetClientes()
        {
            XElement raiz = GetRootNode("Clientes.xml");
            IEnumerable<XElement> clientes = GetElements(raiz, "Cliente");

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