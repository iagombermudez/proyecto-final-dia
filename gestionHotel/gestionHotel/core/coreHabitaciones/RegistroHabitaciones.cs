using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace gestionHotel.core.coreHabitaciones
{
    public class RegistroHabitaciones : ObservableCollection<Habitacion>
    {
        
        public RegistroHabitaciones():base(new List<Habitacion>())
        {
        }

        public RegistroHabitaciones(List<Habitacion> list) : base(list)
        {
        }
        public void RemoveHabitacion(Habitacion x) => 
            base.Remove(x);
        public int Length => 
            this.Count;

        public void RemoveHabitacionForIndex(int i) => 
            base.RemoveAt(i);

        public Habitacion[] RegistroHabitacionToArray => 
            this.ToArray();
        public void AddHabitacion(Habitacion nueva) =>
            base.Add(nueva);
            
            
            
        public Habitacion BuscarHabitacion(int numHabitacion)
        {
            foreach (var h in this.ToArray())
            {
                if (h.Id == numHabitacion)
                {
                    return h;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
           
    }
}