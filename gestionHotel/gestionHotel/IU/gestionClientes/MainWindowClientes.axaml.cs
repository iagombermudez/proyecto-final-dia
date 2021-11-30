using gestionHotel.core.coreClientes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace gestionHotel.IU.gestionClientes
{
    public class MainWindowClientes : Window
    {
        public MainWindowClientes()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var opExit = this.FindControl<MenuItem>( "OpExit" );
            var opSave = this.FindControl<MenuItem>( "OpGuardar" );
            var opInsert = this.FindControl<MenuItem>( "OpInsert" );
            var btInsert = this.FindControl<Button>( "BtInsert" );
            var opDel = this.FindControl<MenuItem>( "OpDel" );
            var btDel = this.FindControl<Button>( "BtDel" );
            var opMod = this.FindControl<MenuItem>( "OpMod" );
            var btMod = this.FindControl<Button>( "BtMod" );
            var dtClientes = this.FindControl<DataGrid>( "DtClientes" );

            opExit.Click += (_, _) => this.OnExit();
            opSave.Click += (_, _) => this.OnSave();
            btInsert.Click += (_, _) => this.OnInsert(); 
            opInsert.Click += (_, _) => this.OnInsert();
            btDel.Click += (_, _) => this.OnDel((Cliente) dtClientes.SelectedItem);
            opDel.Click += (_, _) => this.OnDel((Cliente) dtClientes.SelectedItem);
            btMod.Click += (_, _) => this.OnMod((Cliente) dtClientes.SelectedItem);
            opMod.Click += (_, _) => this.OnMod((Cliente) dtClientes.SelectedItem);
            dtClientes.SelectionChanged += (_, _) => this.OnSelected();
            
            this.Closed += (_, _) => this.OnSave();
            
            this.RegistroClientes = RegistroClientes.RecuperaXml();
            dtClientes.Items = this.RegistroClientes;
            
        }

        void InitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            AvaloniaXamlLoader.Load(this);
        }
        
        void OnSelected()
        {
            var dtClientes = this.FindControl<DataGrid>( "DtClientes" );
            var lblDesc = this.FindControl<Label>( "LblDesc" );
            
            Cliente cliente = (Cliente) dtClientes.SelectedItem;
            lblDesc.Content = $"{cliente}";
            
        }
        
        void OnSave()
        {
            this.RegistroClientes.GuardaXml();
        }
        


        void OnExit()
        {
            this.OnSave();
            this.Close();
        }
        
        async void OnInsert()
        {
            var insertView = new InsertViewClientes();
            await insertView.ShowDialog( this );

            if ( !insertView.IsCancelled ) {

                if (!this.RegistroClientes.ExisteDni(insertView.Dni))
                {
                    this.RegistroClientes.Add(
                        new Cliente(insertView.Dni, insertView.Nombre, insertView.Telefono,
                            insertView.Email, insertView.Direccion));
                }
                else
                {
                    var confirmar = new GeneralMessage("Ya existe un cliente con este Dni, desea modificalo?", true);
                    await confirmar.ShowDialog( this );
                    
                    if (!confirmar.IsCancelled)
                    {
                        OnMod(RegistroClientes.BuscarPorDni(insertView.Dni));
                    }
                    
                }
            }
            
        }

        async void OnDel(Cliente cliente)
        {
            var confirmar = new GeneralMessage("Está seguro de que desea eliminar el cliente?", true);
            await confirmar.ShowDialog( this );

            if (!confirmar.IsCancelled)
            {
                RegistroClientes.Remove(cliente);
            }
            
        }
        
        async void OnMod(Cliente cliente)
        {
            
            var modifyView = new ModifyViewClientes(cliente);
            await modifyView.ShowDialog( this );

            if ( !modifyView.IsCancelled ) {

                this.RegistroClientes.Add(
                        new Cliente(cliente.Dni, modifyView.Nombre, modifyView.Telefono,
                            modifyView.Email, modifyView.Direccion));
                this.RegistroClientes.Remove(cliente);

            
            }
            
            modifyView.Close();
        }
        
        public RegistroClientes RegistroClientes {
            get;
        }
    }
}
