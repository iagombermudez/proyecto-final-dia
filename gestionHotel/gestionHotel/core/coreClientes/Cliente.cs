using System;
using System.Text;
using System.Xml.Linq;

namespace gestionHotel.core.coreClientes
{
    public class Cliente
    {
        public Cliente(string dni, string nombre, int telefono, string email, string direccion)
        {
            this.Dni = dni;
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.Email = email;
            this.Direccion = direccion;
        }

        public string Dni
        {
            get;
        }
        public string Nombre
        {
            get;
            set;
        }
        public int Telefono
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        
        public string Direccion
        {
            get;
            set;
        }

        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();

            toret.AppendLine("DNI: " + Dni);
            toret.AppendLine("Nombre: " + Nombre);
            toret.AppendLine("Telefono: " + Telefono);
            toret.AppendLine("Email: " + Email);
            toret.AppendLine("Direccion Postal: " + Direccion);

            return toret.ToString();
        }

        public XElement ToXml()
        {
            return(new XElement(EtqCliente,
                                new XAttribute( EtqDni, Dni),
                                new XAttribute( EtqNombre, Nombre),
                                new XAttribute( EtqTelefono, Telefono),
                                new XAttribute( EtqEmail, Email),
                                new XAttribute( EtqDireccion, Direccion)));
        }
        
        public static Cliente FromXml(XElement node)
        {
            string dni = (string) node.Attribute( EtqDni )!;
            string nombre = (string) node.Attribute( EtqNombre )!;
            int telefono = Convert.ToInt32((string) node.Attribute( EtqTelefono )!);
            string email = (string) node.Attribute( EtqEmail )!;
            string direccion = (string) node.Attribute( EtqDireccion )!;
            
            return new Cliente( dni, nombre, telefono, email, direccion );
        }

        public const string EtqCliente = "Cliente";
        public const string EtqDni = "Dni";
        public const string EtqNombre = "Nombre";
        public const string EtqTelefono = "Telefono";
        public const string EtqEmail = "Email";
        public const string EtqDireccion = "Direccion";


    }
    
   
}