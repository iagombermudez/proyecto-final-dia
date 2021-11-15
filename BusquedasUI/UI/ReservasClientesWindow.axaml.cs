using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BusquedasUI.Core;
using BusquedasUI.Core.Base;
using BusquedasUI.Core.Search;

namespace BusquedasUI.UI
{
    public partial class ReservasClientesWindow : Window
    {
        private List<Reserva> reservasClientes;

        public ReservasClientesWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();

            
#endif

            var grid = this.FindControl<DataGrid>("Grid");
            var reservasYear = this.FindControl<DatePicker>("Year");
            
            reservasYear.PropertyChanged += (_, _) =>
            {
                if (reservasYear.SelectedDate != null)
                {
                    this.OnYearChanged(grid, reservasYear.SelectedDate.Value.Year);
                }
            };
            reservasClientes = ReservasClientes.GetReservas();
            grid.Items = this.reservasClientes;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnYearChanged( DataGrid grid, int year)
        {
            reservasClientes = ReservasClientes.GetReservasPerYear(year);
            grid.Items = reservasClientes;
        }
        
    }
}