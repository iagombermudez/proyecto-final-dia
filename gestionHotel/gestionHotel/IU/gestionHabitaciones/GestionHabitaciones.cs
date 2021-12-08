using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.OpenGL.Angle;
using gestionHotel.core;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.IO;
using gestionHotel.IU;
using gestionHotel.IU.gestionHabitaciones;
using gestionHotel.IU.gestionReservas;
using hotel;
using hotel.IO;

namespace gestionHotel.IU.gestionHabitaciones
{
    public partial class GestionHabitaciones : Window
    {
        
        private RegistroGeneral registroGeneral;
        

        public GestionHabitaciones(RegistroGeneral rg):this()
        {
            
            this.registroGeneral = rg;
            
            var opExit = this.FindControl<MenuItem>( "OpExit" );
            var opInsertar = this.FindControl<Button>("OpInsert");
            var dtHabitaciones = this.FindControl<DataGrid>("DtHabitaciones");
            var opGuardar = this.FindControl<MenuItem>("OpGuardar");
            var opModificar = this.FindControl<Button>("btModificar");
            var opEliminar = this.FindControl<Button>("btEliminar");
            var btRes = this.FindControl<Button>( "BtRes" );
            
                
            //Para que el datagrid se cargue con los datos del txt.
            dtHabitaciones.Items = this.registroGeneral.H;
            opGuardar.Click += (_, _) => this.OnSave();
            opInsertar.Click += (_, _) => this.Insertar();
            opEliminar.Click += (_, _) => this.Eliminar(dtHabitaciones.SelectedIndex);
            opModificar.Click += (_, _) => this.Modificar(dtHabitaciones.SelectedIndex);
            opExit.Click += (_, _) => this.OnExit();
            btRes.Click += (_, _) => this.OnRes((Habitacion) dtHabitaciones.SelectedItem);
            
            this.Closed += (_, _) => this.OnSave();
            
        }
        public GestionHabitaciones()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

        }
        private void OpSalir()
        {
            this.OnSave();
            this.Close();
        }
        
        void OnSave()
        {
            new XmlGeneral(registroGeneral).GuardarInfoGeneral("infoGeneral.xml");
        }
        

        void OnExit()
        {
            new XmlGeneral(registroGeneral).GuardarInfoGeneral("infoGeneral.xml");
            this.Close();
        }
        
        private async void Insertar() { await new InsertarHabitacion(this.registroGeneral.H).ShowDialog(this); }

        private void Eliminar(int position)
        {
            if (position != -1)
            {
                try
                {
                    this.registroGeneral.H.RemoveAt(position);
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
            if (position != -1) { new InsertarHabitacion(this.registroGeneral.H,this.registroGeneral.H[position]).ShowDialog(this); }
            else { new GeneralMessage("Debes seleccionar una fila antes",false).Show(); }
        }

        private RegistroHabitaciones OnLoad(string nf) {  return XmlRegistroHabitaciones.RecuperarXML(nf); }
        
        private void OnUpdateCount()
        {
            var count = this.FindControl<Label>("LbNumReservas");
            count.Content=this.registroGeneral.H.Length.ToString();
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

                await new InsertarReserva(this.registroGeneral.R,this.registroGeneral.C, habitacion).ShowDialog(this);
                
            }
            
        }
    }
}