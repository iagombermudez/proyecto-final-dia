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
using gestionReservas.core;

namespace gestionHotel.IU.gestionHabitaciones
{
    public partial class InsertarHabitacion : Window
    {
        
        private RegistroHabitaciones habitaciones;
        private Habitacion h;
        public InsertarHabitacion(RegistroHabitaciones habitaciones):this()
        {
            this.habitaciones = habitaciones;
            this.Boton("Insertar"); 
        }
        
        public InsertarHabitacion(RegistroHabitaciones habitaciones, Habitacion h):this()
        {
            this.habitaciones = habitaciones;
            this.h = h;
            this.RellenarDatos();
            
            var tbId = this.FindControl<TextBox>("tbId");
            tbId.IsEnabled = false;
            var dpTypeComboBox = this.FindControl<DockPanel>("dpTypeComboBox");
            dpTypeComboBox.IsVisible = false;
            var dpType = this.FindControl<DockPanel>("dpType");
            dpType.IsVisible = true;
            var tbType = this.FindControl<TextBox>("tbType");
            tbType.Text = h.Tipo;
            
            this.Boton("Guardar");
        }
        
        public InsertarHabitacion()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void RellenarDatos()
        {
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
            id.Text = this.h.Id.ToString();
            tipo.SelectedItem = this.h.Tipo;
            fechaRenovacion.SelectedDate = this.h.FechaRenovacion;
            fechaReserva.SelectedDate = this.h.FechaReserva;
            wifi.IsChecked = this.h.Wifi;
            cajaFuerte.IsChecked = this.h.CajaFuerte;
            miniBar.IsChecked = this.h.MiniBar;
            bano.IsChecked = this.h.Bano;
            cocina.IsChecked = this.h.CajaFuerte;
            tv.IsChecked = this.h.Tv;
        }
        private void Boton(string op)
        {
            var btInsertarHabitacion = this.FindControl<Button>("btInsertarHabitacion");
            //.Content: Obtiene o establece el contenido que se mostrará. En nuestro caso Guardar cambios.
            btInsertarHabitacion.Content = op;
            btInsertarHabitacion.Click += (_,_) => this.Seleccion();
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

                        this.habitaciones.AddHabitacion(datosHabitacion);
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
        
        private void Modificar()
        {
            if (this.CamposVacios())
            {
                var id = this.FindControl<TextBox>("tbId");
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

                if (this.EsNumerico(id.Text))
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