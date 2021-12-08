using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.coreClientes;
using gestionHotel.core.coreHabitaciones;
using gestionHotel.core.coreReservas;
using gestionHotel.core.coreReservas.IO;

namespace gestionHotel.IU.gestionReservas
{
    public partial class MenuReservas : Window
    {
        //private RegistroClientes c;
        //private RegistroHabitaciones h;
        //private RegistroReservas listaReservas;

        public MenuReservas()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            //List<Cliente> clientes = new List<Cliente>();
            //List<Habitacion> habitacions = new List<Habitacion>();
            RegistroClientes clientes = new RegistroClientes();
            RegistroHabitaciones habitaciones = new RegistroHabitaciones();
                

            
            var opInsertar = this.FindControl<MenuItem>("OpInsert");
            var dtReservas = this.FindControl<DataGrid>("DtReservas");
            var opGuardar = this.FindControl<MenuItem>("OpGuardar");
            var opModificar = this.FindControl<Button>("btModificar");
            var opEliminar = this.FindControl<Button>("btEliminar");
            var opSalir = this.FindControl<MenuItem>("OpExit");
            var opFactura = this.FindControl<Button>("btFactura");
                
            dtReservas.Items = RegistroGeneral.Reservas;
                
            opGuardar.Click += (_, _) => this.OnSave("reservas.txt");
            opInsertar.Click += (_, _) => this.OnInsert();
            opEliminar.Click += (_, _) => this.OnDelete(dtReservas.SelectedIndex);
            opModificar.Click += (_, _) => this.OnModify(dtReservas.SelectedIndex);
            opFactura.Click += (_, _) => this.onGenerateReceipt(dtReservas.SelectedIndex);
            opSalir.Click += (_, _) => this.OnExit();
            
        }

        private void onGenerateReceipt(int position)
        {
            if (position != -1)
            {

                string texto = RegistroGeneral.Reservas[position].GetFactura;
                this.showReceipt(texto,RegistroGeneral.Reservas[position].IdReserva);

            }
            else
            {
                new GeneralMessage("Debes seleccionar una fila antes",false).Show();
            }
        }

        private void showReceipt(string texto,int id)
        {
            VisualizacionFactura dialog = new VisualizacionFactura(texto,id);
            dialog.ShowDialog(this);
        }

        private void OnExit()
        {
            this.Close();
        }

        private async void OnDelete(int position)
        {
            if (position != -1)
            {
                try
                { 
                    GeneralMessage dialog = new GeneralMessage("Seguro que deseas eliminar esta reserva", true);
                    await dialog.ShowDialog(this);
                    if (!dialog.IsCancelled)
                    {
                        RegistroGeneral.Reservas.RemoveAt(position);
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
            else
            {
                new GeneralMessage("Debes seleccionar una fila antes",false).Show();
            }
            
        }

        private void OnModify(int position)
        {
            if (position != -1)
            {
                
                new InsertarReserva(RegistroGeneral.Reservas,position,RegistroGeneral.Clientes,RegistroGeneral.Habitaciones).ShowDialog(this);
            }
            else
            {
                new GeneralMessage("Debes seleccionar una fila antes",false).Show();
            }
        }

        private void OnInsert()
        { 
            new InsertarReserva(RegistroGeneral.Reservas,RegistroGeneral.Clientes,RegistroGeneral.Habitaciones).ShowDialog(this);
            //new InsertarReserva(this.listaReservas,this.c,this.h).ShowDialog(this);
        }
        
        private void OnSave(string nf)
        {
            XmlRegistroReservas toXml = new XmlRegistroReservas(RegistroGeneral.Reservas);
            toXml.Guardar(nf);
        }
        
        private RegistroReservas OnLoad(string nf)
        {
            //NECESITA REFERENCIA A LISTA DE CLIENTES Y DE HABITACIONES
            return XmlRegistroReservas.RecuperarXML(nf,RegistroGeneral.Habitaciones,RegistroGeneral.Clientes); //toXml = new XmlRegistroReservas(this.listaReservas);
            
        }
        private void OnUpdateCount()
        {
            var count = this.FindControl<Label>("LbNumReservas");
            count.Content=RegistroGeneral.Reservas.Length.ToString();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        
    }
}