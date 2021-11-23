using System;

namespace gestionHotel.core.coreHabitaciones
{
    public class Habitacion
    {
        public Habitacion(int id, string tipo, DateTime fechaRenovacion, DateTime fechaReserva, bool wifi, bool cajaFuete, bool miniBar, bool bano, bool cocina, bool tv)
        {
            Id = id;
            Tipo = tipo;
            FechaRenovacion = fechaRenovacion;
            FechaReserva = fechaReserva;
            Wifi = wifi;
            CajaFuerte = cajaFuete;
            MiniBar = miniBar;
            Bano = bano;
            Cocina = cocina;
            Tv = tv;
        }

        public int Id
        {
            get;
           
        }

        public int Piso
        {
            get
            {
                return calcularPiso();
            }
        }

        public int calcularPiso()
        {
            string identificador = Id.ToString();
            string p = identificador.Substring(0, 1);
            int piso = Convert.ToInt32(p);
            return piso;
        }
        public string Tipo
        {
            get;
        }
        public DateTime FechaRenovacion
        {
            get;
            set;
        }
        public DateTime FechaReserva
        {
            get;
            set;
        }
        public bool Wifi
        {
            get;
            set;
        }
        public bool CajaFuerte
        {
            get;
            set;
        }
        public bool MiniBar
        {
            get;
            set;
        }
        public bool Bano
        {
            get;
            set;
        }
        public bool Cocina
        {
            get;
            set;
        }
        public bool Tv
        {
            get;
            set;
        }

        public string ToString()
        {
            return "Información de la habitacion" +
                   "\n " + Id + "\n" + Piso + "\n" + Tipo + "\n" + FechaRenovacion
                   + "\n" + FechaReserva + "\n Wifi: " + Wifi + "\n Caja Fuerte: " + CajaFuerte
                   + "\n Mini-bar: " + MiniBar + "\n Baño: " + Bano + "\n Cocina: " + Cocina + "\n TV: " + Tv;
        }
    }
}