using System;
using gestionHotel.core.coreClientes;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.coreReservas;
using JetBrains.Annotations;

namespace gestionHotel.core
{
    public static class RegistroGeneral
    {
        public static RegistroReservas registroReservas = new RegistroReservas();
        public static RegistroHabitaciones registroHabitaciones = new RegistroHabitaciones();
        public static RegistroClientes registroClientes = new RegistroClientes();
        //añadir clientes

        [NotNull]
        public static RegistroReservas Reservas
        {
            get => registroReservas;

            set => registroReservas = value;
        }

        [NotNull]
        public static RegistroHabitaciones Habitaciones
        {
            get => registroHabitaciones;
            set => registroHabitaciones = value;
        }


        public static RegistroClientes Clientes
        {
            get => registroClientes;
            set => registroClientes = value;
        }
    }
}