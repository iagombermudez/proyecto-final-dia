// AvVisualViajes (c) 2021 Baltasar MIT License <jbgarcia@uvigo.es>


using System;
using gestionHotel.core.coreClientes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace gestionHotel.IU.gestionClientes {
    public class ModifyViewClientes : Window
    {
        
        public ModifyViewClientes()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public ModifyViewClientes(Cliente cliente):this()
        {

            this.FindControl<TextBox>("EdDni").Text = cliente.Dni;
            this.FindControl<TextBox>("EdNom").Text = cliente.Nombre;
            this.FindControl<TextBox>("EdTelf").Text = cliente.Telefono.ToString();
            this.FindControl<TextBox>("EdEmail").Text = cliente.Email;
            this.FindControl<TextBox>("EdDir").Text = cliente.Direccion;
            
            var btOk = this.FindControl<Button>( "BtOk" );
            var btCancel = this.FindControl<Button>( "BtCancel" );
            
            btOk.Click += (_, _) => this.OnOk();
            btCancel.Click += (_, _) => this.OnExit();
            this.Closed += (_, _) => this.OnExit();
            
            this.IsCancelled = true;
        }

        
        void OnOk()
        {
            this.IsCancelled = false;
            this.OnExit();
        }

        void OnExit()
        {
            this.Close();
        }

        void InitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            AvaloniaXamlLoader.Load(this);
        }
        
        
        public string Nombre {
            get => this.FindControl<TextBox>( "EdNom" ).Text.Trim();
        }
        
        public int Telefono {
            get => Convert.ToInt32(this.FindControl<TextBox>( "EdTelf" ).Text.Trim());
        }
        
        public string Email {
            get => this.FindControl<TextBox>( "EdEmail" ).Text.Trim();
        }
        
        public string Direccion {
            get => this.FindControl<TextBox>( "EdDir" ).Text.Trim();
        }
        
        public bool IsCancelled {
            get;
            private set;
        }
        
    }
}
