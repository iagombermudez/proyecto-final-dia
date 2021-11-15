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
    public partial class DisponibilidadWindow : Window
    {
        private List<Habitacion> habitaciones;
        private const int DiasAObservar = 5;

        public DisponibilidadWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            habitaciones = HabitacionesVacias.GetHabitacionesVacias(0);
            
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
            habitaciones = HabitacionesVacias.GetHabitacionesVacias(index);
            grid.Items = this.habitaciones;
        }
        
        
    }
}