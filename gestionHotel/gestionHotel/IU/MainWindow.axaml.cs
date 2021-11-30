using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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
        }

        private async void OpenHabitaciones()
        {
            await new GestionHabitaciones().ShowDialog(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}