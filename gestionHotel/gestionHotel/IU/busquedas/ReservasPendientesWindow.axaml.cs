using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreReservas;

namespace gestionHotel.IU.busquedas
{
    public partial class ReservasPendientesWindow : Window
    {
        private Reserva[] reservas;
        private const int DiasAObservar = 5;

        public ReservasPendientesWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();

            
#endif

            reservas = RegistroGeneral.Reservas.GetReservasPendientes();
            var grid = this.FindControl<DataGrid>("Grid");
            grid.Items = this.reservas;


        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        
    }
}