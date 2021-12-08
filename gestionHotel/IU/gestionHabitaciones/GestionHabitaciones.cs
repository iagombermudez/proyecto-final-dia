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
        public GestionHabitaciones()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var opInsertar = this.FindControl<Button>("OpInsert");
            var dtHabitaciones = this.FindControl<DataGrid>("DtHabitaciones");
            var opGuardar = this.FindControl<MenuItem>("OpGuardar");
            var opModificar = this.FindControl<Button>("btModificar");
            var opEliminar = this.FindControl<Button>("btEliminar");
                
            //Para que el datagrid se cargue con los datos del txt.
            dtHabitaciones.Items = RegistroGeneral.Habitaciones;
            opGuardar.Click += (_, _) => this.OnSave("infoGeneral.xml");
            opInsertar.Click += (_, _) => this.Insertar();
            opEliminar.Click += (_, _) => this.Eliminar(dtHabitaciones.SelectedIndex);
            opModificar.Click += (_, _) => this.Modificar(dtHabitaciones.SelectedIndex);

        }
        private void OnSave(string nf)
        {
            //XmlRegistroHabitaciones toXml = new XmlRegistroHabitaciones(this.registroGeneral.H);
            XmlGeneral toXml = new XmlGeneral();
            toXml.GuardarInfoGeneral(nf);
        }
        
        private async void Insertar() { await new InsertarHabitacion().ShowDialog(this); }

        private void Eliminar(int position)
        {
            if (position != -1)
            {
                try
                {
                    RegistroGeneral.Habitaciones.RemoveAt(position);
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
            if (position != -1) { new ModificarHabitacion( RegistroGeneral.Habitaciones.RegistroHabitacionToArray[position]).ShowDialog(this); }
            else { new GeneralMessage("No se ha seleccionado una fila",false).Show(); }
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
    }
}