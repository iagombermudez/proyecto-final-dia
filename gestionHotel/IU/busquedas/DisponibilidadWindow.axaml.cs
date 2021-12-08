using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreHabitaciones;

namespace gestionHotel.IU.busquedas
{
    public partial class DisponibilidadWindow : Window
    {
        private Habitacion[] habitaciones;
        private const int DiasAObservar = 5;

        public DisponibilidadWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            habitaciones = RegistroGeneral.Habitaciones.RegistroHabitacionToArray;
            
            var grid = this.FindControl<DataGrid>("Grid");
            var piso = this.FindControl<ComboBox>("PisoCb");
            
            piso.PropertyChanged += (_, _) => this.OnPisoPropertyChanged(piso.SelectedIndex, grid);
            
            grid.Items = this.habitaciones;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnPisoPropertyChanged(int index, DataGrid grid)
        {
            habitaciones = habitaciones;
            grid.Items = this.habitaciones;
        }
        
        
    }
}