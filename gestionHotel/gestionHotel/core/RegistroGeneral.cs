using System;
using gestionHotel.core.coreClientes;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.coreReservas;
using JetBrains.Annotations;

namespace gestionHotel.core
{
    public class RegistroGeneral
    {
        private RegistroReservas r;
        private RegistroHabitaciones h;
        private RegistroClientes c;
        //añadir clientes

        public RegistroGeneral(RegistroHabitaciones h)
        {
            this.h = h;
            
        }
        public RegistroGeneral()
        {
            this.r = new RegistroReservas();
            this.h = new RegistroHabitaciones();

        }

        [NotNull]
        public RegistroReservas R
        {
            get => r;
            
        }

        [NotNull]
        public RegistroHabitaciones H
        {
            get => h;
        }

        public RegistroClientes C
        {
            get => c;
        }
    }
}