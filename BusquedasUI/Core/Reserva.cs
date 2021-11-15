using System.Text;

namespace BusquedasUI.Core
{
    public class Reserva
    {
        public string Id { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public string FechaEntrada { get; set; }
        public string FechaSalida { get; set; }
        public bool Garaje { get; set; }
        public double Importe{ get; set; }
        public double Iva{ get; set; }

        public Reserva(string id, string tipo, string cliente,
            string fechaEntrada, string fechaSalida,
            bool garaje, double importe, double iva)
        {
            this.Id = id;
            this.Tipo = tipo;
            this.Cliente = cliente;
            this.FechaEntrada = fechaEntrada;
            this.FechaSalida = fechaSalida;
            this.Garaje = garaje;
            this.Importe = importe;
            this.Iva = iva;
        }

        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();
            toret.Append("\nId: " + this.Id);
            toret.Append("\nTipo: " + this.Tipo);
            toret.Append("\nCliente: " + this.Cliente);
            toret.Append("\nFechaEntrada: " + this.FechaEntrada);
            toret.Append("\nFechaSalida: " + this.FechaSalida);
            toret.Append("\nGaraje: " + this.Garaje);
            toret.Append("\nImporte: " + this.Importe);
            toret.Append("\nIva: " + this.Iva);

            return toret.ToString();
        }
    }
}