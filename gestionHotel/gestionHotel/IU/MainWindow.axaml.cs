using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core.coreClientes;
using gestionHotel.IU.gestionClientes;
using gestionHotel.IU.gestionHabitaciones;

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
        
        public RegistroClientes RegistroClientes {
            get;
        }
    }
}