using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.coreReservas;
using ScottPlot.Avalonia;

namespace gestionHotel.IU.gestionHabitaciones
{

    public partial class GraficoHabitacion : Window
    {
        private Habitacion h;
        private RegistroReservas registroReservas;
        
        public GraficoHabitacion(){}
        public GraficoHabitacion(Habitacion h)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            this.registroReservas=RegistroGeneral.Reservas;
            this.h = h;
            this.GraficaMes(h);
            this.GraficaAnho(h);
            
        }

        ////Reservas por meses
        private void GraficaMes(Habitacion h)
        {
            AvaPlot grafica = this.Find<AvaPlot>("GraficaMes");
            List<int> meses = new List<int>(); //lista con el mes de cada reserva
            
            foreach (Reserva r in RegistroGeneral.Reservas)
            {
                if(r.Habitacion.Id.Equals(h.Id))
                    meses.Add(r.FechaEntrada.Month);
            }

            //creamos pares <key (mes), count (nº de reservas en ese mes)>
            var reservas = meses
                .GroupBy(s => s)
                .Select(reserva => new {Key = reserva.Key, Count = reserva.Count()});


            double[] values = new double[12];
            double[] mes = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
            string[] mesString =
            {
                "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre",
                "Noviembre", "Diciembre"
            };

            foreach (var a in reservas)
            {
                values[a.Key - 1] = a.Count;
            }

            grafica.Plot.Clear();
            var pl1 = grafica.Plot.AddBar(values, mes);
            pl1.ShowValuesAboveBars = true;
            pl1.FillColor = System.Drawing.Color.Indigo;
            grafica.Plot.XTicks(mes, mesString);
            grafica.Plot.SetAxisLimits(yMin: 0);
            grafica.Plot.SaveFig("bar_positions.png");
        }
        
        ////Reservas por años  
        private void GraficaAnho(Habitacion h)
        {
            AvaPlot grafica = this.Find<AvaPlot>("GraficaAnho");
            List<int> anhos = new List<int>(); //lista con el año de cada reserva
            
            foreach (Reserva r in RegistroGeneral.Reservas)
            {
                if(r.Habitacion.Id.Equals(h.Id))
                    anhos.Add(r.FechaEntrada.Year);
            }
            
            //<key (año), count (nº de reservas en ese año)>
            var reservas  = anhos
                .GroupBy(s => s)
                .Select(reserva => new { Key = reserva.Key, Count = reserva.Count() });
            
            //Desde el año de reserva más antiguo hasta el más reciente
            int min = anhos.Min();
            int max = anhos.Max();
            int numAños = max - min;
            
            double[] values = new double[numAños+1];
            double[] años = new double[numAños+1]; 
            string[] añoString = new string[numAños+1]; 
            
            int minn = min;
            for (int i = 0; i <= numAños; i++)
            {
                añoString[i] = minn.ToString();
                años[i] = minn;
                minn++;
            }
            
            foreach (var a in reservas)
            {
                values[a.Key-min] = a.Count;
            }
            
            grafica.Plot.Clear();
            grafica.Plot.AddBar(values, años).ShowValuesAboveBars = true;
            grafica.Plot.XTicks(años, añoString);
            //grafica.Plot.YLabel("Número de reservas");
            grafica.Plot.SetAxisLimits(yMin: 0);
            grafica.Plot.SaveFig("bar_positions.png");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}