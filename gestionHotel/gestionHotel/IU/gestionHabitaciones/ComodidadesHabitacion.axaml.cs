using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreHabitaciones;
using ScottPlot.Avalonia;

namespace gestionHotel.IU.gestionHabitaciones
{

    public partial class ComodidadesHabitacion : Window
    {
        public ComodidadesHabitacion()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            var tbRoomCount = this.FindControl<TextBlock>("TbRoomCount");
            tbRoomCount.Text = "Numero total de habitaciones: " + RegistroGeneral.Habitaciones.Count;
            
            this.OnDraw();
            
        }

        private void OnDraw()
        {
            AvaPlot avaPlot1 = this.Find<AvaPlot>("SpGraph");

            double[] valores;
            double[] posiciones = {0, 1, 2, 3, 4, 5};
            string[] nombres = { "WiFi", "Caja fuerte", "Minibar", "Bano", "Cocina", "TV"};

            valores = CalcularValores();

            avaPlot1.Plot.AddBar(valores,posiciones).ShowValuesAboveBars = true;
            avaPlot1.Plot.XTicks(posiciones, nombres);
            avaPlot1.Plot.SetAxisLimits(yMin: 0);
            avaPlot1.Refresh();
        }

        private double[] CalcularValores()
        {
            RegistroHabitaciones rg = RegistroGeneral.Habitaciones;
            double[] valores = {0,0,0,0,0,0};
            
            foreach (Habitacion h in rg)
            {
                bool[] comodidades = h.Comodidades();
                for (int i = 0; i < 6; i++)
                {
                    if (comodidades[i])
                    {
                        valores[i] = valores[i] + 1;
                    }
                }
            }

            return valores;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}