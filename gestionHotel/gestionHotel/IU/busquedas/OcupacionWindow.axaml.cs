using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreHabitaciones;

namespace gestionHotel.IU.busquedas
{
    public partial class OcupacionWindow : Window
    {
        private Habitacion[] habitaciones;
        private const int DiasAObservar = 5;

        public OcupacionWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            habitaciones = RegistroGeneral.Habitaciones.RegistroHabitacionToArray;
            
            var grid = this.FindControl<DataGrid>("Grid");
            var dtPicker = this.FindControl<DatePicker>("DtPicker");
            
            dtPicker.SelectedDateChanged += (_, _) => this.verHabitacionesOcupadas(dtPicker.SelectedDate.Value.DateTime, grid);
            
            grid.Items = this.habitaciones;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void verHabitacionesOcupadas(DateTime dia, DataGrid grid)
        {
            Habitacion[] habitaciones = RegistroGeneral.Habitaciones.RegistroHabitacionToArray;
            List<Habitacion> habitacionesOcupadas = new List<Habitacion>();
            for (int i = 0; i < habitaciones.Length; i++)
            {
                if (habitaciones[i].EstaReservada(dia))
                {
                    habitacionesOcupadas.Add(habitaciones[i]);
                }
            }

            grid.Items = habitacionesOcupadas;
        }


    }
}