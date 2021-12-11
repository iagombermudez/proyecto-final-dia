using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreClientes;
using gestionHotel.core.coreReservas;

namespace gestionHotel.IU.busquedas
{
    public partial class ReservasClientesWindow : Window
    {
        private List<Reserva> reservasClientes;
        private Cliente cliente;

        public ReservasClientesWindow()
        {
        }

        public ReservasClientesWindow(Cliente cliente)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();

            
#endif
            this.cliente = cliente;
            var grid = this.FindControl<DataGrid>("Grid");
            var reservasYear = this.FindControl<DatePicker>("Year");
            
            reservasYear.PropertyChanged += (_, _) =>
            {
                if (reservasYear.SelectedDate != null)
                {
                    this.OnYearChanged(grid, reservasYear.SelectedDate.Value.Year);
                }
            };
            Reserva[] reservas = RegistroGeneral.Reservas.RegistroReservasToArray;
            reservasClientes = new List<Reserva>();
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i].Cliente.Dni == cliente.Dni)
                {
                    reservasClientes.Add(reservas[i]);
                }
            }
            
            grid.Items = this.reservasClientes;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnYearChanged( DataGrid grid, int year)
        {
            Reserva[] reservas = RegistroGeneral.Reservas.RegistroReservasToArray;
            reservasClientes = new List<Reserva>();
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i].Cliente.Dni == cliente.Dni && reservas[i].FechaEntrada.Year == year)
                {
                    reservasClientes.Add(reservas[i]);
                }
            }
            
            grid.Items = this.reservasClientes;
        }
        
    }
}