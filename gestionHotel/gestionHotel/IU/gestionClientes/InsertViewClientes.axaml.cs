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
            
            btOk.Click += (_, _) => this.OnOk();
            btCancel.Click += (_, _) => this.OnExit();
            this.Closed += (_, _) => this.OnExit();
            
            this.IsCancelled = true;
        }

        void InitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            AvaloniaXamlLoader.Load(this);
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

        public string Dni {
            get
            {
                string toret;
                
                try
                {
                    toret = this.FindControl<TextBox>( "EdDni" ).Text.Trim();
                }
                catch (Exception exc)
                {
                    toret = "";
                }

                return toret;
            }
            
        }
        
        public string Nombre {
            get
            {
                string toret;

                try
                {
                    toret = this.FindControl<TextBox>("EdNom").Text.Trim();
                }
                catch (Exception exc)
                {
                    toret = "";
                }

                return toret;
            }
        }
        
        public int Telefono {
            
            get
            {
                int toret;
                
                try
                {
                    toret = Convert.ToInt32(this.FindControl<TextBox>( "EdTelf" ).Text.Trim());
                }
                catch (Exception exc)
                {
                    toret = -1;
                }

                return toret;
            }
        }
        
        public string Email {
            
            get
            {
                string toret;

                try
                {
                    toret = this.FindControl<TextBox>( "EdEmail" ).Text.Trim();
                }
                catch (Exception exc)
                {
                    toret = "";
                }

                return toret;
            }
        }
        
        public string Direccion {
            get
            {
                string toret;

                try
                {
                    toret = this.FindControl<TextBox>( "EdDir" ).Text.Trim();

                }
                catch (Exception exc)
                {
                    toret = "";
                }

                return toret;
            }
        }

        
        public bool IsCancelled {
            get;
            private set;
        }
        
    }
}
