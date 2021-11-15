using System;
using System.Collections.Generic;
using System.Text;

namespace BusquedasUI.Core
{
    public class Habitacion
    {
        public string Id { get; set; }
        public string Tipo { get; set; }
        public string FechaUltimaRenovacion { get; set; }
        public string FechaUltimaReserva { get; set; }
        public List<string> Comodidades { get; set; }

        public Habitacion(string id, string tipo,
            string fechaUltimaRenovacion, string fechaUltimaReserva,
            List<string> comodidades)
        {
            this.Id = id;
            this.Tipo = tipo;
            this.FechaUltimaRenovacion = fechaUltimaRenovacion;
            this.FechaUltimaReserva = fechaUltimaReserva;
            this.Comodidades = comodidades;
        }

        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();
            toret.Append("\nId: " + this.Id);
            toret.Append("\nTipo: " + this.Tipo);
            toret.Append("\nFechaUltimaRenovacion: " + this.FechaUltimaRenovacion);
            toret.Append("\nFechaUltimaReserva: " + this.FechaUltimaReserva);
            toret.Append("\nComodidades: ");
            for (int i = 0; i < this.Comodidades.Count; i++)
            {
                toret.Append("\n\t").Append(this.Comodidades[i]);
            }

            return toret.ToString();
        }

        public int GetNumPiso()
        {
            int numPiso = this.Id[0] - '0';
            return numPiso;
        }
    }
}