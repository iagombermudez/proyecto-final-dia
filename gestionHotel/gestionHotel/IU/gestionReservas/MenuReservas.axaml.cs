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
using gestionHotel.core.IO;
using gestionHotel.IU.busquedas;

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
            var opInsertar = this.FindControl<Button>("OpInsert");
            var dtReservas = this.FindControl<DataGrid>("DtReservas");
            var opGuardar = this.FindControl<MenuItem>("OpGuardar");
            var opModificar = this.FindControl<Button>("btModificar");
            var opEliminar = this.FindControl<Button>("btEliminar");
            var opReservasPendientes = this.FindControl<Button>("btReservasPendientes");
            var opSalir = this.FindControl<MenuItem>("OpExit");
            var opFactura = this.FindControl<Button>("btFactura");
            var btGrafCliente = this.FindControl<Button>("BtGrafCliente");
            
            dtReservas.Items = RegistroGeneral.Reservas;
                
            opGuardar.Click += (_, _) => this.OnSave();
            opInsertar.Click += (_, _) => this.OnInsert();
            opEliminar.Click += (_, _) => this.OnDelete(dtReservas.SelectedIndex);
            opModificar.Click += (_, _) => this.OnModify(dtReservas.SelectedIndex);
            opFactura.Click += (_, _) => this.onGenerateReceipt(dtReservas.SelectedIndex);
            opReservasPendientes.Click += (_, _) => this.OnReservasPendientes();
            btGrafCliente.Click += (_, _) => this.OnGraphClienteInd();
            opSalir.Click += (_, _) => this.OnExit();
            
            this.Closed += (_, _) => this.OnSave();
            
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

        private void showReceipt(string texto,long id)
        {
            VisualizacionFactura dialog = new VisualizacionFactura(texto,id);
            dialog.ShowDialog(this);
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
                
                new InsertarReserva().ShowDialog(this);
            }
            else
            {
                new GeneralMessage("Debes seleccionar una fila antes",false).Show();
            }
        }

        private void OnInsert()
        { 
            new InsertarReserva().ShowDialog(this);
            //new InsertarReserva(this.listaReservas,this.c,this.h).ShowDialog(this);
        }
        
        private void OnExit()
        {
            this.OnSave();
            this.Close();
        }
        
        private 
        
        void OnSave()
        {
            new XmlGeneral().GuardarInfoGeneral("infoGeneral.xml");
        }

        
        private RegistroReservas OnLoad(string nf)
        {
            //NECESITA REFERENCIA A LISTA DE CLIENTES Y DE HABITACIONES
            return XmlRegistroReservas.RecuperarXML(nf,RegistroGeneral.Habitaciones,RegistroGeneral.Clientes); //toXml = new XmlRegistroReservas(this.listaReservas);
            
        }

        private void OnReservasPendientes()
        {
            new ReservasPendientesWindow().ShowDialog(this);
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

        async void OnGraphClienteInd() { await new GrafReservasIndividuales().ShowDialog(this); }
        
        
    }
}