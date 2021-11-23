using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.OpenGL.Angle;
using hotel;
using hotel.IO;

namespace hotelCrud.NETCoreApp
{
    public partial class MainWindow : Window
    {
        
        private RegistroHabitaciones listaHabitaciones;
        
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            try
            {
                listaHabitaciones = this.OnLoad("habitaciones.xml");
            }
            catch (Exception exc)
            {
                listaHabitaciones = new RegistroHabitaciones();
            }
            
            var opInsertar = this.FindControl<Button>("OpInsert");
            var dtHabitaciones = this.FindControl<DataGrid>("DtHabitaciones");
            var opGuardar = this.FindControl<MenuItem>("OpGuardar");
            var opModificar = this.FindControl<Button>("btModificar");
            var opEliminar = this.FindControl<Button>("btEliminar");
                
            //Para que el datagrid se cargue con los datos del txt.
            dtHabitaciones.Items = this.listaHabitaciones;
            opGuardar.Click += (_, _) => this.OnSave("habitaciones.xml");
            opInsertar.Click += (_, _) => this.Insertar();
            opEliminar.Click += (_, _) => this.Eliminar(dtHabitaciones.SelectedIndex);
            opModificar.Click += (_, _) => this.Modificar(dtHabitaciones.SelectedIndex);
            
        }
        private void OnSave(string nf)
        {
            XmlRegistroHabitaciones toXml = new XmlRegistroHabitaciones(this.listaHabitaciones);
            toXml.Guardar(nf);
        }
        
        private async void Insertar() { await new InsertarHabitacion(listaHabitaciones).ShowDialog(this); }

        private void Eliminar(int position)
        {
            if (position != -1)
            {
                try
                {
                    this.listaHabitaciones.RemoveAt(position);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else  { new Message("No se ha seleccionado una fila",false).Show(); }
        }

        private void Modificar(int position)
        {
            if (position != -1) { new InsertarHabitacion(listaHabitaciones,listaHabitaciones[position]).ShowDialog(this); }
            else { new Message("No se ha seleccionado una fila",false).Show(); }
        }

        private RegistroHabitaciones OnLoad(string nf) {  return XmlRegistroHabitaciones.RecuperarXML(nf); }
        
        private void OnUpdateCount()
        {
            var count = this.FindControl<Label>("LbNumReservas");
            count.Content=this.listaHabitaciones.Length.ToString();
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}