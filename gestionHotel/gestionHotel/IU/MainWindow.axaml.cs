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

            XmlGeneral.cargarXML("infoGeneral.xml"); 
            
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
        
    }
}