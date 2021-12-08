using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace gestionHotel.IU.gestionClientes {
    
    public class InsertViewClientes : Window
    {

        public InsertViewClientes()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var btOk = this.FindControl<Button>( "BtOk" );
            var btCancel = this.FindControl<Button>( "BtCancel" );
            
            btOk.Click += (_, _) => this.OnExit();
            btCancel.Click += (_, _) => this.OnCancelClicked();
            this.Closed += (_, _) => this.OnCancelClicked();
            
            this.IsCancelled = false;
        }

        void InitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            AvaloniaXamlLoader.Load(this);
        }

        void OnCancelClicked()
        {
            this.IsCancelled = true;
            this.OnExit();
        }

        void OnExit()
        {
            this.Close();
        }

        public string Dni {
            get => this.FindControl<TextBox>( "EdDni" ).Text.Trim();
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
