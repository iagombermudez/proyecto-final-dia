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

        public RegistroGeneral(RegistroHabitaciones h, RegistroClientes c)
        {
            this.h = h;
            this.c = c;

        }
        public RegistroGeneral()
        {
            this.r = new RegistroReservas();
            this.h = new RegistroHabitaciones();
            this.c = new RegistroClientes();

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