using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BusquedasUI.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var reservasPendientesBtn = this.FindControl<Button>("ReservasPendientesBtn");
            var disponibilidadBtn = this.FindControl<Button>("DisponibilidadBtn");
            var reservasClientesBtn = this.FindControl<Button>("ReservasClientesBtn");
            var reservasHabitacionesBtn = this.FindControl<Button>("ReservasHabitacionesBtn");
            var ocupacionBtn = this.FindControl<Button>("OcupacionBtn");

            reservasPendientesBtn.Click += (_, _) => this.OnReservasPendientesClick();
            disponibilidadBtn.Click += (_, _) => this.OnDisponibilidadClick();
            reservasClientesBtn.Click += (_, _) => this.OnReservasClientesClick();
            reservasHabitacionesBtn.Click += (_, _) => this.OnReservasHabitacionesClick();
            ocupacionBtn.Click += (_, _) => this.OnOcupacionClick();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnReservasPendientesClick()
        {
            new ReservasPendientesWindow().Show();
        }
        
        private void OnReservasClientesClick()
        {
            new ReservasClientesWindow().Show();
        }
        
        private void OnReservasHabitacionesClick()
        {
            new ReservasHabitacionWindow().Show();
        }

        private void OnDisponibilidadClick()
        {
            new DisponibilidadWindow().Show();
        }

        private void OnOcupacionClick()
        {
            new OcupacionWindow().Show();
        }
    }
}