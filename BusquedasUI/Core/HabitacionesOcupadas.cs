using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace BusquedasUI.Core
{
    public class HabitacionesOcupadas
    {
        public static List<Habitacion> GetHabitacionesOcupadas( int piso)
        {
            List<Habitacion> habitacionesSinFiltrar = GetHabitacionesOcupadas();

            return piso == 0 
                ? habitacionesSinFiltrar 
                : habitacionesSinFiltrar.Where(habitacion => habitacion.GetNumPiso() == piso).ToList();
        }

        private static List<Habitacion> GetHabitacionesOcupadas()
        {
            HabitacionXmlReader xmlReader = new HabitacionXmlReader();
            List<Habitacion> habitaciones = xmlReader.GetHabitaciones();
            List<Habitacion> habitacionesVacias = new List<Habitacion>();

            var today = DateTime.Today;
            for (int i = 0; i < habitaciones.Count; i++)
            {
                var fechaUltimaReserva = DateTime.Parse(habitaciones[i].FechaUltimaReserva);
                if (DateTime.Compare(fechaUltimaReserva, today) == 0)
                {
                    habitacionesVacias.Add(habitaciones[i]);
                }
            }
            return habitacionesVacias;
        }
    }
}