using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreReservas;
using ScottPlot.Avalonia;

namespace gestionHotel.IU.gestionReservas
{

    public partial class GrafReservasIndividuales : Window
    {
        public GrafReservasIndividuales()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            var btSearch = this.FindControl<Button>("BtSearch");

            btSearch.Click += (_, _) => this.OnSearch();

        }

        private void OnSearch()
        {
            var tbSearch = this.FindControl<TextBox>("TbClientID");
            var lbWarning = this.FindControl<Label>("LbWarning");
            var cbTimeSelect = this.FindControl<ComboBox>("CbTimeSelect");
            AvaPlot avaPlot1 = this.Find<AvaPlot>("SpGraph");
            avaPlot1.Plot.Clear();
            
            string dni = tbSearch.Text;
            bool flag = false;

            foreach (Reserva r in RegistroGeneral.Reservas)
            {
                if (flag == false && r.Cliente.Dni.Equals(dni))
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                lbWarning.IsVisible = false;

                if (cbTimeSelect.SelectedIndex == 0)
                    this.OnDrawMonth(dni);
                else
                    this.OnDrawYear(dni);

                avaPlot1.IsVisible = true;

            }
            else
            {
                avaPlot1.IsVisible = false;
                lbWarning.IsVisible = true;
            }
        }

        private void OnDrawMonth(string dni)
        {
            AvaPlot avaPlot1 = this.Find<AvaPlot>("SpGraph");

            double[] valores;
            double[] posiciones = {0,1,2,3,4,5,6,7,8,9,10,11};
            string[] nombres = { "Enero","Febrero","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"};

            valores = CalcValueMonth(dni);

            avaPlot1.Plot.AddBar(valores,posiciones).ShowValuesAboveBars = true;
            avaPlot1.Plot.XTicks(posiciones, nombres);
            avaPlot1.Plot.SetAxisLimits(yMin: 0);
            avaPlot1.Refresh();
        }
        
        private void OnDrawYear(string dni)
        {
            AvaPlot avaPlot1 = this.Find<AvaPlot>("SpGraph");

            double[] valores;
            double[] posiciones = {0,1,2,3,4,5,6,7};
            string[] nombres = new string[8];
            
            int temp = DateTime.Today.Year;

            for (int i = 0; i < 8; i++)
            {
                nombres[i] = (temp - 5 + i).ToString();
            }

            valores = CalcValueYear(dni);

            avaPlot1.Plot.AddBar(valores,posiciones).ShowValuesAboveBars = true;
            avaPlot1.Plot.XTicks(posiciones, nombres);
            avaPlot1.Plot.SetAxisLimits(yMin: 0);
            avaPlot1.Refresh();
        }

        private double[] CalcValueMonth(string dni)
        {
            double[] valores ={0,0,0,0,0,0,0,0,0,0,0,0};
            
            foreach (Reserva r in RegistroGeneral.Reservas)
            {
                if (r.Cliente.Dni.Equals(dni))
                {
                    if (r.FechaEntrada.Year == DateTime.Today.Year)
                    {
                        int temp = r.FechaEntrada.Month;
                        temp--;
                        valores[temp] += 1;
                    }
                }
            }

            return valores;
        }
        
        private double[] CalcValueYear(string dni)
        {
            double[] valores = {0,0,0,0,0,0,0,0};
            
            foreach (Reserva r in RegistroGeneral.Reservas)
            {
                if (r.Cliente.Dni.Equals(dni))
                {
                    int temp = r.FechaEntrada.Year;
                    if (temp > DateTime.Today.Year - 6 && temp < DateTime.Today.Year + 3)
                    {
                        temp = temp - DateTime.Today.Year + 5;
                        valores[temp] += 1;
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