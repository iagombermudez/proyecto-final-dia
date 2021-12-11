using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.coreReservas;

namespace gestionHotel.IU.busquedas
{
    public partial class DisponibilidadWindow : Window
    {
        private const int DiasAObservar = 5;

        public DisponibilidadWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            var grid = this.FindControl<DataGrid>("Grid");
            var piso = this.FindControl<ComboBox>("PisoCb");
            
            piso.PropertyChanged += (_, _) => this.OnPisoPropertyChanged(piso.SelectedIndex, grid);

            grid.Items = GetHabitacionesVacias();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnPisoPropertyChanged(int index, DataGrid grid)
        {
            grid.Items = GetHabitacionesVacias(index);
        }

        private List<Habitacion> GetHabitacionesVacias()
        {
            Habitacion[] habitaciones = RegistroGeneral.Habitaciones.RegistroHabitacionToArray;
            List<Habitacion> habitacionesVacias = new ();
            for (int i = 0; i < habitaciones.Length; i++)
            {
                if (habitaciones[i].FechaReserva > DateTime.Today)
                {
                    habitacionesVacias.Add(habitaciones[i]);
                }
            }

            return habitaciones.ToList();
        }
        
        private List<Habitacion> GetHabitacionesVacias(int piso)
        {
            Habitacion[] habitaciones = RegistroGeneral.Habitaciones.RegistroHabitacionToArray;
            List<Habitacion> habitacionesVacias = new List<Habitacion>();
            for (int i = 0; i < habitaciones.Length; i++)
            {
                if (habitaciones[i].FechaReserva < DateTime.Today
                && habitaciones[i].Piso == piso)
                {
                    habitacionesVacias.Add(habitaciones[i]);
                }
            }

            return habitacionesVacias;
        }
        

        private Reserva[] GetReservasHabitacion(Habitacion habitacion)
        {
            Reserva[] reservas = RegistroGeneral.Reservas.RegistroReservasToArray;
            List<Reserva> reservasHabitacion = new List<Reserva>();
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i].Habitacion.Id == habitacion.Id)
                {
                    reservasHabitacion.Add(reservas[i]);
                }
            }

            return reservasHabitacion.ToArray();
        }
        
        
    }
}