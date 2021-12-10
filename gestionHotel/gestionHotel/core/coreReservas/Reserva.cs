using System;
using System.IO;
using gestionHotel.core.coreClientes;
using gestionHotel.core.coreHabitaciones;
using Microsoft.VisualBasic;

namespace gestionHotel.core.coreReservas
{
    public class Reserva
    {
        public Reserva(Cliente cliente, Habitacion habitacion, DateTime fechaEntrada, DateTime fechaSalida, int iva, bool usaGaraje, double precioPorDia,string tipo)
        {
            Cliente = cliente;
            Habitacion = habitacion;
            FechaEntrada = fechaEntrada;
            FechaSalida = fechaSalida;
            IVA = iva;
            UsaGaraje = usaGaraje;
            PrecioPorDia = precioPorDia;
            this.IdReserva = this.GenerateID(fechaEntrada, habitacion);
            this.Tipo = tipo;
        }

        private int GenerateID(DateTime fechaEntrada, Habitacion habitacion)
        {
            string id= fechaEntrada.Year+""+fechaEntrada.Month+""+fechaEntrada.Day+""+this.Habitacion.Id+"";
            return Int32.Parse(id);
        }
        public Cliente Cliente
        {
            get;
        }

        public string Tipo
        {
            get;
        }

        public Habitacion Habitacion
        {
            get;
        }

        public DateTime FechaEntrada
        {
            get;
            set;
        }

        public DateTime FechaSalida
        {
            get;
            set;
        }

        public int IVA
        {
            get;
            set;
        }

        public bool UsaGaraje
        {
            get;
            set;
        }

        public Double PrecioPorDia
        {
            get;
            set;
        }

        public int IdReserva
        {
            get;
           
        }

        public string GetFactura
        {
            get
            {
                
                string factura="\n ############# FACTURA #############\n";
                factura += "\n - ID  de la reserva: "+this.IdReserva+"\n";
                factura += "\n - DNI cliente: "+this.Cliente.Dni+"\n";
                factura += "\n - IVA: "+this.IVA+" %\n";
                factura += "\n - Importe por día: "+this.PrecioPorDia+"\n";
                factura += "\n TOTAL : " + this.getTotal() +" "+DIVISA+" \n";
                factura += "\n ##################################";

                return factura;
            }
        }

        private const string DIVISA = "€";

        private double getTotal()
        {
            int numDias = (this.FechaSalida - this.FechaEntrada).Days;
            double sinIVA = numDias * this.PrecioPorDia;
            return sinIVA + (sinIVA * this.IVA/100);
        }
        
        
    }
}