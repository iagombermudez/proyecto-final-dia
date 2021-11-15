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
    public partial class OcupacionWindow : Window
    {
        private List<Habitacion> habitaciones;
        private const int DiasAObservar = 5;

        public OcupacionWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            habitaciones = HabitacionesOcupadas.GetHabitacionesOcupadas(0);
            
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
            habitaciones = HabitacionesOcupadas.GetHabitacionesOcupadas(index);
            grid.Items = this.habitaciones;
        }
        
        
    }
}