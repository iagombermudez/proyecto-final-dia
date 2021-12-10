using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.coreReservas;
using JetBrains.Annotations;

namespace gestionHotel.IU.gestionHabitaciones
{
    public partial class InsertarHabitacion : Window
    {
        private RegistroHabitaciones habitaciones;
        private Habitacion h;
        List<int> idsLibres = new List<int>();
      
        public InsertarHabitacion()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            idsLibres.Add(101);
            idsLibres.Add(102);
            idsLibres.Add(103);
            idsLibres.Add(201);
            idsLibres.Add(202);
            idsLibres.Add(203);
            idsLibres.Add(301);
            idsLibres.Add(302);
            idsLibres.Add(303);

            foreach (var h in RegistroGeneral.Habitaciones)
            {
                if (idsLibres.Contains(h.Id)) idsLibres.Remove(h.Id);
            }
            
            var cbHabitaciones = this.FindControl<ComboBox>("tbId");
            cbHabitaciones.Items = idsLibres;
            
            this.habitaciones = RegistroGeneral.Habitaciones;
            
            var btInsertarHabitacion = this.FindControl<Button>("btInsertarHabitacion");
            btInsertarHabitacion.Click += (_,_) => this.Insertar();
        }

        private void Insertar()
        {
            if (this.CamposVacios())
            {
                    var id = this.FindControl<ComboBox>("tbId");
                    var tipo = this.FindControl<ComboBox>("cbTipoHabitacion");
                    var fechaRenovacion = this.FindControl<DatePicker>("dpFechaRenovacion");
                    var fechaReserva = this.FindControl<DatePicker>("dpFechaReserva");
                    var wifi = this.FindControl<CheckBox>("cbWifi");
                    var cajaFuerte = this.FindControl<CheckBox>("cbCajaFuerte");
                    var miniBar = this.FindControl<CheckBox>("cbMiniBar");
                    var bano = this.FindControl<CheckBox>("cbBano");
                    var cocina = this.FindControl<CheckBox>("cbCocina");
                    var tv = this.FindControl<CheckBox>("cbTv");

                    int identificador = id.SelectedIndex;
                    identificador = idsLibres[identificador];
                    
                    int index = tipo.SelectedIndex;
                    string type = "";
                    if (index == 0) type = "matrimonial";
                    if (index == 1) type = "doble";
                    if (index == 2) type = "individual";
                    DateTime fRenovacion = fechaRenovacion.SelectedDate.Value.DateTime;
                    DateTime fReserva = fechaReserva.SelectedDate.Value.DateTime;
                    bool tieneWifi = (bool) wifi.IsChecked;
                    bool tieneCajaFuerte = (bool) cajaFuerte.IsChecked;
                    bool tieneMiniBar = (bool) miniBar.IsChecked;
                    bool tieneBano = (bool) bano.IsChecked;
                    bool tieneCocina = (bool) cocina.IsChecked;
                    bool tieneTv = (bool) tv.IsChecked;

                    if (this.EsNumerico(identificador.ToString()))
                    {
                        Habitacion datosHabitacion = new Habitacion(identificador, type, fRenovacion, fReserva,
                            tieneWifi, tieneCajaFuerte, tieneMiniBar, tieneBano, tieneCocina, tieneTv);

                        this.habitaciones.AddHabitacion(datosHabitacion);
                        this.Close();
                    }
                    else
                    {
                        new GeneralMessage("Error en alguno de los datos", false).Show();
                        
                    }
            }
            else
            { 
                new GeneralMessage("Algún campo es vacío", false).Show();
            }
        }
        
        private void Modificar()
        {
            if (this.CamposVacios())
            {
                var id = this.FindControl<TextBox>("tbIdModificacion");
                var tipo = this.FindControl<TextBox>("tbType");
                var fechaRenovacion = this.FindControl<DatePicker>("dpFechaRenovacion");
                var fechaReserva = this.FindControl<DatePicker>("dpFechaReserva");
                var wifi = this.FindControl<CheckBox>("cbWifi");
                var cajaFuerte = this.FindControl<CheckBox>("cbCajaFuerte");
                var miniBar = this.FindControl<CheckBox>("cbMiniBar");
                var bano = this.FindControl<CheckBox>("cbBano");
                var cocina = this.FindControl<CheckBox>("cbCocina");
                var tv = this.FindControl<CheckBox>("cbTv");

                int identificador = Convert.ToInt32(id.Text);

                string type = tipo.Text;
                DateTime fRenovacion = fechaRenovacion.SelectedDate.Value.DateTime;
                DateTime fReserva = fechaReserva.SelectedDate.Value.DateTime;
                bool tieneWifi = (bool) wifi.IsChecked;
                bool tieneCajaFuerte = (bool) cajaFuerte.IsChecked;
                bool tieneMiniBar = (bool) miniBar.IsChecked;
                bool tieneBano = (bool) bano.IsChecked;
                bool tieneCocina = (bool) cocina.IsChecked;
                bool tieneTv = (bool) tv.IsChecked;

                if (this.EsNumerico(identificador.ToString()))
                {
                    Habitacion datosHabitacion = new Habitacion(identificador,type,fRenovacion,fReserva, tieneWifi, tieneCajaFuerte, tieneMiniBar, tieneBano, tieneCocina, tieneTv );

                    this.habitaciones.RemoveHabitacion(this.h);
                    this.habitaciones.AddHabitacion(datosHabitacion);
                    this.Close();
                }
                else
                {
                    new GeneralMessage("Error en alguno de los datos",false).Show();
                }
            }
            else
            {
                new GeneralMessage("Algún campo es vacío",false).Show();
            }
        }

        private bool EsNumerico(string value)
        {
            bool toret = true;
            try
            {
                int v=Int32.Parse(value);
            }
            catch (Exception exc)
            {
                toret = false;
            }

            return toret;
        }
        
        private bool CamposVacios()
        {
            var id= this.FindControl<ComboBox>("tbId");
            var tipo= this.FindControl<ComboBox>("cbTipoHabitacion");
            var fechaRenovacion= this.FindControl<DatePicker>("dpFechaRenovacion");
            var fechaReserva= this.FindControl<DatePicker>("dpFechaReserva");
            var wifi= this.FindControl<CheckBox>("cbWifi");
            var cajaFuerte= this.FindControl<CheckBox>("cbCajaFuerte");
            var miniBar= this.FindControl<CheckBox>("cbMiniBar");
            var bano= this.FindControl<CheckBox>("cbBano");
            var cocina= this.FindControl<CheckBox>("cbCocina");
            var tv= this.FindControl<CheckBox>("cbTv");

                    if (id.ToString() == null || tipo.SelectedIndex == null
                                    || fechaRenovacion.SelectedDate == null 
                                    || fechaReserva.SelectedDate == null || 
                                    wifi.IsChecked == null || cajaFuerte.IsChecked == null || miniBar.IsChecked == null || 
                                    bano.IsChecked == null || cocina.IsChecked == null || tv.IsChecked == null)
                    {  
                        return false;
                    }
            return true;
        }
        private void Seleccion()
        {
            if (this.h == null) this.Insertar(); 
            else
            {
                this.Modificar();
            }
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}