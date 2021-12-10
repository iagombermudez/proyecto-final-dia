using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace gestionHotel.core.coreReservas
{
    public class RegistroReservas : ObservableCollection<Reserva>
    {
        public RegistroReservas():base(new List<Reserva>())
        {
        }

        public RegistroReservas(List<Reserva> list) : base(list)
        {
        }

        public void AddReserva(Reserva nueva) => base.Add(nueva);

        public void RemoveReserva(Reserva x) => base.Remove(x);

        public void RemoveReservaForIndex(int i) => base.RemoveAt(i);

        public Reserva[] RegistroReservasToArray => this.ToArray();
        public int Length => this.Count;
        
        public Reserva[] GetReservasPendientes()
        {
            Reserva[] reservas = this.RegistroReservasToArray;
            DateTime today = DateTime.Today;
            IEnumerable<Reserva> reservasPendientes = reservas
                .Where(reserva => reserva.FechaEntrada > today)
                .Select(reserva => reserva);
            return reservasPendientes.ToArray();
        }
    }
}