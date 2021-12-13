using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.OpenGL.Angle;
using gestionHotel.core;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.IO;
using gestionHotel.IU;
using gestionHotel.IU.busquedas;
using gestionHotel.IU.gestionHabitaciones;
using gestionHotel.IU.gestionReservas;
using hotel;
using hotel.IO;

namespace gestionHotel.IU.gestionHabitaciones
{
    public partial class GestionHabitaciones : Window
    {
        

        public GestionHabitaciones()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var opExit = this.FindControl<MenuItem>( "OpExit" );
            var opInsertar = this.FindControl<Button>("OpInsert");
            var dtHabitaciones = this.FindControl<DataGrid>("DtHabitaciones");
            var opGuardar = this.FindControl<MenuItem>("OpGuardar");
            var opModificar = this.FindControl<Button>("btModificar");
            var opEliminar = this.FindControl<Button>("btEliminar");
            var opVerReservas = this.FindControl<Button>("btVerReservas");
            var opVerDisponibilidad = this.FindControl<Button>("btVerDisponibilidad");
            var opVerOcupadas = this.FindControl<Button>("btVerOcupadas");
            var btRes = this.FindControl<Button>( "BtRes" );
            var btCom = this.FindControl<Button>("btComodidades");
            var btGraf = this.FindControl<Button>("BtGraf");
                
            //Para que el datagrid se cargue con los datos del txt.
            dtHabitaciones.Items = RegistroGeneral.Habitaciones;
            opGuardar.Click += (_, _) => this.OnSave();
            opInsertar.Click += (_, _) => this.Insertar();
            opEliminar.Click += (_, _) => this.Eliminar(dtHabitaciones.SelectedIndex);
            opModificar.Click += (_, _) => this.Modificar(dtHabitaciones.SelectedIndex);
            opVerReservas.Click += (_, _) => this.VerReservasHabitacion((Habitacion) dtHabitaciones.SelectedItem);
            opVerDisponibilidad.Click += (_, _) => this.VerDisponibilidad();
            opVerOcupadas.Click += (_, _) => this.VerOcupadas();
            opExit.Click += (_, _) => this.OnExit();
            btRes.Click += (_, _) => this.OnRes((Habitacion) dtHabitaciones.SelectedItem);
            btCom.Click += (_, _) => this.OnGraphComodity();
            btGraf.Click += (_, _) => this.OnGraficaHabitacion((Habitacion) dtHabitaciones.SelectedItem);
            
            this.Closed += (_, _) => this.OnSave();
            
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
        

        void OnExit()
        {
            new XmlGeneral().GuardarInfoGeneral("infoGeneral.xml");
            this.Close();
        }
        
        private async void Insertar() { await new InsertarHabitacion().ShowDialog(this); }

        async private void Eliminar(int position)
        {
            if (position != -1)
            {
                try
                {
                    var confirmar = new GeneralMessage("Está seguro de que desea eliminar la habitacion?", true);
                    await confirmar.ShowDialog( this );
                    
                    if (!confirmar.IsCancelled)
                    {
                        RegistroGeneral.Habitaciones.RemoveAt(position);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else  { new GeneralMessage("Debes seleccionar una fila antes",false).Show(); }
        }

        private void Modificar(int position)
        {
            if (position != -1) { new ModificarHabitacion(RegistroGeneral.Habitaciones.RegistroHabitacionToArray[position]).ShowDialog(this); }
            else { new GeneralMessage("Debes seleccionar una fila antes",false).Show(); }
        }

        private RegistroHabitaciones OnLoad(string nf) {  return XmlRegistroHabitaciones.RecuperarXML(nf); }
        
        private void OnUpdateCount()
        {
            var count = this.FindControl<Label>("LbNumReservas");
            count.Content=RegistroGeneral.Habitaciones.Length.ToString();
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        async void OnRes(Habitacion habitacion)
        {
            if (habitacion == null)
            {
                new GeneralMessage("No se ha seleccionado una fila",false).Show();
            }
            else
            {

                await new InsertarReserva(habitacion).ShowDialog(this);
                
            }
            
        }

        async void VerReservasHabitacion(Habitacion habitacion)
        {
            if (habitacion == null)
            {
                new GeneralMessage("No se ha seleccionado una fila",false).Show();
            }
            else
            {
                await new ReservasHabitacionWindow(habitacion).ShowDialog(this);
            }
        }

        async void VerDisponibilidad()
        {
            await new DisponibilidadWindow().ShowDialog(this);
        }
        
        async void VerOcupadas()
        {
            await new OcupacionWindow().ShowDialog(this);
        }
        
        async void OnGraphComodity() { await new ComodidadesHabitacion().ShowDialog(this); }
        
        async void OnGraficaHabitacion(Habitacion habitacion)
        {
            if (habitacion == null)
            {
                new GeneralMessage("No se ha seleccionado una habitacion",false).Show();
            }
            else
            {
                await new GraficoHabitacion(habitacion).ShowDialog(this);
            }
        }
    }
}