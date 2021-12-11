using System;

namespace gestionHotel.core.coreHabitaciones
{
    public class Habitacion
    {
        public Habitacion()
        { }

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

        /// <summary>
        /// El id corresponde a 3 digitos de los cuales el primero corresponde a la planta
        /// y los 2 siguientes a la habitación. Al ser un edificio de 3 alturas y 3 habitaciones
        /// por planta solo puede tener los ids siguientes
        ///
        /// 101/102/103
        /// 201/202/203
        /// 301/302/303
        /// </summary>
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

        public override string ToString()
        {
            return Id.ToString();
        }

        public bool[] Comodidades()
        {
            bool[] toret = new bool[6];
            toret[0] = Wifi;
            toret[1] = CajaFuerte;
            toret[2] = MiniBar;
            toret[3] = Bano;
            toret[4] = Cocina;
            toret[5] = Tv;
            return toret;
        }
    }
}