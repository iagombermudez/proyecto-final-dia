using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BusquedasUI.Core;

namespace BusquedasUI.UI
{
    public partial class ReservasHabitacionWindow : Window
    {
        private List<Reserva> reservasHabitaciones;

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
            reservasHabitaciones = ReservasClientes.GetReservas();
            grid.Items = this.reservasHabitaciones;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnYearChanged( DataGrid grid, int year)
        {
            reservasHabitaciones = ReservasClientes.GetReservasPerYear(year);
            grid.Items = reservasHabitaciones;
        }
        
    }
}