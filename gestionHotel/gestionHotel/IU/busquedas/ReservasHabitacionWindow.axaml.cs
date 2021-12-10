using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreReservas;

namespace gestionHotel.IU.busquedas
{
    public partial class ReservasHabitacionWindow : Window
    {
        private Reserva[] reservasHabitaciones;

        public ReservasHabitacionWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();

            
#endif

            var grid = this.FindControl<DataGrid>("Grid");
            var reservasYear = this.FindControl<DatePicker>("Year");
            
            reservasYear.PropertyChanged += (_, _) =>
            {
                if (reservasYear.SelectedDate != null) this.OnYearChanged(grid, reservasYear.SelectedDate.Value.Year);
            };
            reservasHabitaciones = RegistroGeneral.Reservas.RegistroReservasToArray;
            grid.Items = this.reservasHabitaciones;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnYearChanged( DataGrid grid, int year)
        {
            reservasHabitaciones = reservasHabitaciones;
            grid.Items = reservasHabitaciones;
        }
        
    }
}