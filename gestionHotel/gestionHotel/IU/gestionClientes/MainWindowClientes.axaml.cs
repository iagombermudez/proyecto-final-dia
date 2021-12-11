using System;
using gestionHotel.core.coreClientes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionHotel.core;
using gestionHotel.core.IO;
using gestionHotel.IU.busquedas;
using gestionHotel.IU.gestionReservas;

namespace gestionHotel.IU.gestionClientes
{
    public class MainWindowClientes : Window
    {
        private RegistroClientes registroClientes;
        
        public MainWindowClientes()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            this.registroClientes = RegistroGeneral.Clientes;

            var opExit = this.FindControl<MenuItem>( "OpExit" );
            var opSave = this.FindControl<MenuItem>( "OpGuardar" );
            var btInsert = this.FindControl<Button>( "BtInsert" );
            var btDel = this.FindControl<Button>( "BtDel" );
            var btMod = this.FindControl<Button>( "BtMod" );
            var btRes = this.FindControl<Button>( "BtRes" );
            var btVerReservas = this.FindControl<Button>( "BtVerReservas" );
            var dtClientes = this.FindControl<DataGrid>( "DtClientes" );
            var btGrafCliente = this.FindControl<Button>("BtGrafCliente");

            opExit.Click += (_, _) => this.OnExit();
            opSave.Click += (_, _) => this.OnSave();
            btInsert.Click += (_, _) => this.OnInsert(); 
            btDel.Click += (_, _) => this.OnDel((Cliente) dtClientes.SelectedItem);
            btMod.Click += (_, _) => this.OnMod((Cliente) dtClientes.SelectedItem);
            btVerReservas.Click += (_, _) => this.OnReservasClientes((Cliente) dtClientes.SelectedItem);
            btRes.Click += (_, _) => this.OnRes((Cliente) dtClientes.SelectedItem);
            btGrafCliente.Click += (_, _) => this.OnGraphClienteInd();
            
            this.Closed += (_, _) => this.OnSave();
            
            dtClientes.Items = this.registroClientes;

        }

        void InitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            AvaloniaXamlLoader.Load(this);
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
        
        async void OnInsert()
        {
            
            var insertView = new InsertViewClientes();
            await insertView.ShowDialog( this );

            if (!insertView.IsCancelled )
            {
                if (!this.registroClientes.ExisteDni(insertView.Dni))
                {
                    this.registroClientes.Add(
                        new Cliente(insertView.Dni, insertView.Nombre, insertView.Telefono,
                            insertView.Email, insertView.Direccion));
                    
                }
                else
                {
                    var confirmar = new GeneralMessage("Ya existe un cliente con este Dni, desea modificalo?", true);
                    await confirmar.ShowDialog( this );
                    
                    if (!confirmar.IsCancelled)
                    {
                        OnMod(this.registroClientes.BuscarPorDni(insertView.Dni));
                    }
                    
                }
            }
            
        }

        async void OnDel(Cliente cliente)
        {

            if (cliente == null)
            {
                new GeneralMessage("Debes seleccionar una fila antes",false).Show();
            }
            else
            {
                var confirmar = new GeneralMessage("Está seguro de que desea eliminar el cliente?", true);
                await confirmar.ShowDialog( this );

                if (!confirmar.IsCancelled)
                {
                    this.registroClientes.Remove(cliente);
                }
            }
            
        }
        
        async void OnMod(Cliente cliente)
        {
            if (cliente == null)
            {
                new GeneralMessage("Debes seleccionar una fila antes",false).Show();
            }
            else
            {

                var modifyView = new ModifyViewClientes(cliente);
                await modifyView.ShowDialog(this);

                if (!modifyView.IsCancelled)
                {

                    this.registroClientes.Add(
                        new Cliente(cliente.Dni, modifyView.Nombre, modifyView.Telefono,
                            modifyView.Email, modifyView.Direccion));
                    this.registroClientes.Remove(cliente);


                }
                modifyView.Close();
            }

            
        }
        async void OnRes(Cliente cliente)
        {
            if (cliente == null)
            {
                new GeneralMessage("Debes seleccionar una fila antes",false).Show();
            }
            else
            {

                await new InsertarReserva(cliente).ShowDialog(this);
                
            }
            
        }

        async void OnReservasClientes(Cliente cliente)
        {
            if (cliente == null)
            {
                new GeneralMessage("Debes seleccionar una fila antes", false).Show();
            }
            else
            {
                await new ReservasClientesWindow(cliente).ShowDialog(this);
            }
        }
        
        async void OnGraphClienteInd() { await new GrafReservasIndividuales().ShowDialog(this); }
       
    }
}
