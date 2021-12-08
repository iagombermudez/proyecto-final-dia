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

            var opSalir = this.FindControl<MenuItem>("OpExit");
            opSalir.Click += (_, _) => this.OpSalir();
            
            this.Closed += (_, _) => this.OpSalir();

            
            try
            {
                this.rg = XmlGeneral.cargarXML("infoGeneral.xml"); 
            }
            catch (Exception e)
            {
                this.rg = new RegistroGeneral();
            }
            
            //CARGAR DATRAGRID CON RESULTADO DE BUSQUEDA DE HABITACIONES LIBRES
            var dtHabitacionesLibres = this.FindControl<DataGrid>("DtHabitacionesLibres");
            
            //dtHabitacionesLibres.Items=
        }

        private void OpSalir()
        {
            this.OnSave();
            this.Close();
        }
        
        void OnSave()
        {
            new XmlGeneral(rg).GuardarInfoGeneral("infoGeneral.xml");
        }
        
        private async void OpVerReservas()
        {
            await new MenuReservas(this.rg).ShowDialog(this);
        }

        private async void OpenHabitaciones()
        {
            await new GestionHabitaciones(this.rg).ShowDialog(this);
        }
        private async void OpenClientes()
        {
            await new MainWindowClientes(this.rg).ShowDialog(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
    }
}