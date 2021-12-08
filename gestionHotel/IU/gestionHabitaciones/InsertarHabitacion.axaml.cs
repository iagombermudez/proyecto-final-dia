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

namespace gestionHotel.IU.gestionHabitaciones
{
    public partial class InsertarHabitacion : Window
    {
        public InsertarHabitacion()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var btInsertarHabitacion = this.FindControl<Button>("btInsertarHabitacion");
            btInsertarHabitacion.Click += (_,_) => this.Insertar();
        }
        
        

        private void RellenarDatos()
        {
            Habitacion habitacion = new ();
            //Relaciono un componente con una variable.
            var id = this.FindControl<TextBox>("tbId");
            var tipo= this.FindControl<ComboBox>("cbTipoHabitacion");
            var fechaRenovacion= this.FindControl<DatePicker>("dpFechaRenovacion");
            var fechaReserva= this.FindControl<DatePicker>("dpFechaReserva");
            var wifi= this.FindControl<CheckBox>("cbWifi");
            var cajaFuerte= this.FindControl<CheckBox>("cbCajaFuerte");
            var miniBar= this.FindControl<CheckBox>("cbMiniBar");
            var bano= this.FindControl<CheckBox>("cbBano");
            var cocina= this.FindControl<CheckBox>("cbCocina");
            var tv= this.FindControl<CheckBox>("cbTv");

            //Se hace tipo.Text para pasarlo a texto
            id.Text =habitacion.Id.ToString();
            tipo.SelectedItem =habitacion.Tipo;
            fechaRenovacion.SelectedDate =habitacion.FechaRenovacion;
            fechaReserva.SelectedDate =habitacion.FechaReserva;
            wifi.IsChecked =habitacion.Wifi;
            cajaFuerte.IsChecked =habitacion.CajaFuerte;
            miniBar.IsChecked =habitacion.MiniBar;
            bano.IsChecked =habitacion.Bano;
            cocina.IsChecked =habitacion.CajaFuerte;
            tv.IsChecked =habitacion.Tv;
        }
        private void Boton(string op)
        {
            
        }
        
        private void Insertar()
        {
            if (this.CamposVacios())
            {
                    var id = this.FindControl<TextBox>("tbId");
                    var tipo = this.FindControl<ComboBox>("cbTipoHabitacion");
                    var fechaRenovacion = this.FindControl<DatePicker>("dpFechaRenovacion");
                    var fechaReserva = this.FindControl<DatePicker>("dpFechaReserva");
                    var wifi = this.FindControl<CheckBox>("cbWifi");
                    var cajaFuerte = this.FindControl<CheckBox>("cbCajaFuerte");
                    var miniBar = this.FindControl<CheckBox>("cbMiniBar");
                    var bano = this.FindControl<CheckBox>("cbBano");
                    var cocina = this.FindControl<CheckBox>("cbCocina");
                    var tv = this.FindControl<CheckBox>("cbTv");

                    int identificador = Convert.ToInt32(id.Text);
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

                    if (this.EsNumerico(id.Text))
                    {
                        Habitacion datosHabitacion = new Habitacion(identificador, type, fRenovacion, fReserva,
                            tieneWifi, tieneCajaFuerte, tieneMiniBar, tieneBano, tieneCocina, tieneTv);

                        RegistroGeneral.Habitaciones.AddHabitacion(datosHabitacion);
                        this.Close();
                    }
                    else
                    {
                        new GeneralMessage("Error en alguno de los dato", false).Show();
                    }
            }
            else
            { 
                new GeneralMessage("Algún campo es vacío", false).Show();
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
            var id= this.FindControl<TextBox>("tbId");
            var tipo= this.FindControl<ComboBox>("cbTipoHabitacion");
            var fechaRenovacion= this.FindControl<DatePicker>("dpFechaRenovacion");
            var fechaReserva= this.FindControl<DatePicker>("dpFechaReserva");
            var wifi= this.FindControl<CheckBox>("cbWifi");
            var cajaFuerte= this.FindControl<CheckBox>("cbCajaFuerte");
            var miniBar= this.FindControl<CheckBox>("cbMiniBar");
            var bano= this.FindControl<CheckBox>("cbBano");
            var cocina= this.FindControl<CheckBox>("cbCocina");
            var tv= this.FindControl<CheckBox>("cbTv");

                    if (id.Text == null || tipo.SelectedIndex == null
                                    || fechaRenovacion.SelectedDate == null 
                                    || fechaReserva.SelectedDate == null || 
                                    wifi.IsChecked == null || cajaFuerte.IsChecked == null || miniBar.IsChecked == null || 
                                    bano.IsChecked == null || cocina.IsChecked == null || tv.IsChecked == null)
                    {  
                        return false;
                    }
            return true;
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}