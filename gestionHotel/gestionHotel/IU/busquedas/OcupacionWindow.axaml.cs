using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreHabitaciones;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace gestionHotel.IU.busquedas
{
    public partial class OcupacionWindow : Window
    {
        private static DataGrid grid = null!;
        private static CheckBox filterYear = null!;
        private static DatePicker dtPicker = null!;

        public OcupacionWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            grid = this.FindControl<DataGrid>("Grid");
            dtPicker = this.FindControl<DatePicker>("DtPicker");
            filterYear = this.FindControl<CheckBox>("yearFilter");
            dtPicker.SelectedDate = DateTime.Today;
            
            dtPicker.SelectedDateChanged += (_, _) => this.VerHabitacionesOcupadas();
            filterYear.PropertyChanged += (_, _) => this.VerHabitacionesOcupadas();
            this.VerHabitacionesOcupadas();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void VerHabitacionesOcupadas()
        {
            if (filterYear.IsChecked != null && filterYear.IsChecked.Value)
            {
                FiltrarPorAnho();
            }
            else
            {
                FiltrarPorDia();
            }
            
        }

        private void FiltrarPorDia()
        {
            Habitacion[] habitaciones = RegistroGeneral.Habitaciones.RegistroHabitacionToArray;
            List<Habitacion> habitacionesOcupadas = new List<Habitacion>();
            for (int i = 0; i < habitaciones.Length; i++)
            {
                if (habitaciones[i].EstaReservada(dtPicker.SelectedDate.Value.DateTime))
                {
                    habitacionesOcupadas.Add(habitaciones[i]);
                }
            }

            grid.Items = habitacionesOcupadas;
        }

        private void FiltrarPorAnho()
        {
            Habitacion[] habitaciones = RegistroGeneral.Habitaciones.RegistroHabitacionToArray;
            List<Habitacion> habitacionesOcupadas = new List<Habitacion>();
            for (int i = 0; i < habitaciones.Length; i++)
            {
                if (habitaciones[i].EstaReservada(dtPicker.SelectedDate.Value.DateTime.Year))
                {
                    habitacionesOcupadas.Add(habitaciones[i]);
                }
            }

            grid.Items = habitacionesOcupadas;
        }


    }
}