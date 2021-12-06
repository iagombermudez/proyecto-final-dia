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
            
            var opInsertar = this.FindControl<Button>("OpInsert");
            var dtHabitaciones = this.FindControl<DataGrid>("DtHabitaciones");
            var opGuardar = this.FindControl<MenuItem>("OpGuardar");
            var opModificar = this.FindControl<Button>("btModificar");
            var opEliminar = this.FindControl<Button>("btEliminar");
                
            //Para que el datagrid se cargue con los datos del txt.
            dtHabitaciones.Items = this.registroGeneral.H;
            opGuardar.Click += (_, _) => this.OnSave("infoGeneral.xml");
            opInsertar.Click += (_, _) => this.Insertar();
            opEliminar.Click += (_, _) => this.Eliminar(dtHabitaciones.SelectedIndex);
            opModificar.Click += (_, _) => this.Modificar(dtHabitaciones.SelectedIndex);

        }
        public GestionHabitaciones()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

        }
        private void OnSave(string nf)
        {
            //XmlRegistroHabitaciones toXml = new XmlRegistroHabitaciones(this.registroGeneral.H);
            XmlGeneral toXml = new XmlGeneral(this.registroGeneral);
            toXml.GuardarInfoGeneral(nf);
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
            else  { new GeneralMessage("No se ha seleccionado una fila",false).Show(); }
        }

        private void Modificar(int position)
        {
            if (position != -1) { new InsertarHabitacion(this.registroGeneral.H,this.registroGeneral.H[position]).ShowDialog(this); }
            else { new GeneralMessage("No se ha seleccionado una fila",false).Show(); }
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
    }
}