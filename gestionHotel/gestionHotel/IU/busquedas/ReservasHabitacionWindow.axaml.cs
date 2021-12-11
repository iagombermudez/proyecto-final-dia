using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.coreReservas;

namespace gestionHotel.IU.busquedas
{
    public partial class ReservasHabitacionWindow : Window
    {
        private List<Reserva> reservasHabitaciones;
        private Habitacion habitacion;

        
        public ReservasHabitacionWindow(){}
        public ReservasHabitacionWindow(Habitacion habitacion)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();

            
#endif

            this.habitacion = habitacion;
            var grid = this.FindControl<DataGrid>("Grid");
            var reservasYear = this.FindControl<DatePicker>("Year");
            
            reservasYear.PropertyChanged += (_, _) =>
            {
                if (reservasYear.SelectedDate != null) this.OnYearChanged(grid, reservasYear.SelectedDate.Value.Year);
            };
            Reserva[] reservas = RegistroGeneral.Reservas.RegistroReservasToArray;
            reservasHabitaciones = new List<Reserva>();
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i].Habitacion.Id == habitacion.Id)
                {
                    reservasHabitaciones.Add(reservas[i]);
                }
            }
            grid.Items = this.reservasHabitaciones;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnYearChanged( DataGrid grid, int year)
        {
            Reserva[] reservas = RegistroGeneral.Reservas.RegistroReservasToArray;
            reservasHabitaciones = new List<Reserva>();
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i].Habitacion.Id == habitacion.Id 
                    && reservas[i].FechaEntrada.Year == year)
                {
                    reservasHabitaciones.Add(reservas[i]);
                }
            }
            grid.Items = this.reservasHabitaciones;
        }
        
    }
}