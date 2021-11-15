using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BusquedasUI.Core;
using BusquedasUI.Core.Base;
using BusquedasUI.Core.Search;

namespace BusquedasUI.UI
{
    public partial class ReservasPendientesWindow : Window
    {
        private List<Reserva> reservas;
        private const int DiasAObservar = 5;

        public ReservasPendientesWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();

            
#endif
            
            reservas = ReservasPendientes.GetReservasUltimosDias(DiasAObservar);
            var grid = this.FindControl<DataGrid>("Grid");
            grid.Items = this.reservas;


        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        
    }
}