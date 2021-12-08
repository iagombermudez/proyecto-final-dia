using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace gestionHotel.core.coreClientes
{
    public class RegistroClientes: ObservableCollection<Cliente>
        {
            
        public RegistroClientes():base(new List<Cliente>())
        {
        }
        
        public bool ExisteDni(string dni)
        {
            return this.Any((x) => x.Dni == dni);
        }
        
        public Cliente BuscarPorDni(string dni)
        {
            return this.First((x) => x.Dni == dni);
        }

        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();

            int i = 1;
            foreach (var cliente in this)
            {
                toret.AppendLine("Cliente: " + i++ );
                toret.AppendLine("----------------------------");
                toret.AppendLine(cliente.ToString());
            }
            
            return toret.ToString();
        }
        
        
        public XElement ToXml()
        {
            var toret = new XElement( EtqClientes );
            
            foreach(Cliente cliente in this) {
                toret.Add( cliente.ToXml() );
            }

            return toret;
        }
        
        
        public static RegistroClientes FromXml(XElement root)
        {
            string rootTag = root?.Name.ToString() ?? "";
            var toret = new RegistroClientes();

            if (root != null && rootTag == EtqClientes)
            {
                try
                {
                    var listaClientes = root.Elements(EtqCliente);

                    new List<XElement>(listaClientes!).ForEach(
                        node => toret.Add(Cliente.FromXml(node)));
                }
                catch (XmlException)
                {
                    toret.Clear();
                }
                catch (IOException)
                {
                    toret.Clear();
                }
                
            }

            return toret;
            
        }
        
        public const string EtqClientes = "Clientes";
        public const string EtqCliente = "Cliente";

        }
    
}