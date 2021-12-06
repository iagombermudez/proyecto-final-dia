using System;
using System.Linq;
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
        private RegistroGeneral rg;
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

            
            try
            {
                this.rg = XmlGeneral.cargarXML("infoGeneral.xml"); 
            }
            catch (Exception e)
            {
                this.rg = new RegistroGeneral();
            }
            
            
                       
        }

        private async void OpVerReservas()
        {
            await new MenuReservas().ShowDialog(this);
        }

        private async void OpenHabitaciones()
        {
            await new GestionHabitaciones(this.rg).ShowDialog(this);
        }
        private async void OpenClientes()
        {
            await new MainWindowClientes().ShowDialog(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public RegistroClientes RegistroClientes {
            get;
        }
    }
}