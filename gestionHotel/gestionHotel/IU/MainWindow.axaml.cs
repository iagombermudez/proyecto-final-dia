using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreClientes;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.IO;
using gestionHotel.IU.gestionClientes;
using gestionHotel.IU.gestionHabitaciones;
using gestionHotel.IU.gestionReservas;

namespace gestionHotel.IU
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var opHabitaciones = this.FindControl<MenuItem>("OpHabitaciones");
            opHabitaciones.Click += (_, _) => this.OpenHabitaciones();

            var opClientes = this.FindControl<MenuItem>("OpClientes");
            opClientes.Click += (_, _) => this.OpenClientes();

            var opVerReservas = this.FindControl<MenuItem>("OpVerReservas");
            opVerReservas.Click += (_, _) => this.OpVerReservas();
            
            var opGrafComodidades = this.FindControl<MenuItem>("OpComodidades");
            opGrafComodidades.Click += (_, _) => this.OnGraphComodity();
            
            var opGrafReservasInd = this.FindControl<MenuItem>("OpReservasCliente");
            opGrafReservasInd.Click += (_, _) => this.OnGraphReservaInd();

            var opSalir = this.FindControl<MenuItem>("OpExit");
            opSalir.Click += (_, _) => this.OpSalir();

            var grid = this.FindControl<DataGrid>("DtHabitacionesLibres");
            
            var opActualizar = this.FindControl<Button>("btActualizar");
            opActualizar.Click += (_, _) => this.VerHabitacionesDisponibles(DateTime.Today, grid);

            var opReservar = this.FindControl<Button>("btReservar");
            opReservar.Click += (_, _) => this.RealizarReserva((Habitacion)grid.SelectedItem);
            
            this.Closed += (_, _) => this.OpSalir();

            try
            {
                XmlGeneral.cargarXML("infoGeneral.xml");
            }
            catch (Exception ex)
            {
                
            }
            
            this.VerHabitacionesDisponibles(DateTime.Today, grid);

            //CARGAR DATRAGRID CON RESULTADO DE BUSQUEDA DE HABITACIONES LIBRES
            var dtHabitacionesLibres = this.FindControl<DataGrid>("DtHabitacionesLibres");
        }

        private void OpSalir()
        {
            this.OnSave();
            this.Close();
        }
        
        void OnSave()
        {
            new XmlGeneral().GuardarInfoGeneral("infoGeneral.xml");
        }
        
        private async void OpVerReservas()
        {
            await new MenuReservas().ShowDialog(this);
        }

        private async void OpenHabitaciones()
        {
            await new GestionHabitaciones().ShowDialog(this);
        }
        private async void OpenClientes()
        {
            await new MainWindowClientes().ShowDialog(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void OnGraphComodity()
        {
            await new ComodidadesHabitacion().ShowDialog(this);
        }
        
        private async void OnGraphReservaInd()
        {
            await new GrafReservasIndividuales().ShowDialog(this);
        }
        
        private void VerHabitacionesDisponibles(DateTime dia, DataGrid grid)
        {
            Habitacion[] habitaciones = RegistroGeneral.Habitaciones.RegistroHabitacionToArray;
            List<Habitacion> habitacionesDisponibles = new List<Habitacion>();
            for (int i = 0; i < habitaciones.Length; i++)
            {
                if (habitaciones[i].EstaDisponible(dia))
                {
                    habitacionesDisponibles.Add(habitaciones[i]);
                }
            }

            grid.Items = habitacionesDisponibles;
            
        }

        private async void RealizarReserva(Habitacion habitacion)
        {
            if (habitacion != null)
            {
                await new InsertarReserva(habitacion).ShowDialog(this);
            }
            else
            {
                var confirmar = new GeneralMessage("No se ha seleccionado una habitación", false);
                await confirmar.ShowDialog( this );
            }
        }


    }
}