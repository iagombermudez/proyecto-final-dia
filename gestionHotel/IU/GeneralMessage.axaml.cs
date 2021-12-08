using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace gestionHotel.IU
{
    public partial class GeneralMessage : Window
    {
        private bool confirmacion=false;
        
        
        public GeneralMessage(string mensaje,bool confirmacion):this()
        {
            var texBlock = this.FindControl<TextBlock>("AboutText");
            texBlock.Text=mensaje;
            this.confirmacion = confirmacion;
            
            var boton = this.FindControl<Button>("salirAyuda");
            if (confirmacion)
            {
                boton.IsVisible = false;
                var btAceptar = this.FindControl<Button>("btAceptar");
                var btCancelar = this.FindControl<Button>("btCancelar");
                btAceptar.IsVisible = true;
                btCancelar.IsVisible = true;
                btAceptar.Click += (_, _) => this.OnAccept();
                btCancelar.Click += (_, _) => this.OnCancel();
                
            }
            else
            {
                boton.Click += (_, _) => this.Close();
            }


        }
        
        
        public GeneralMessage()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.IsCancelled = true;
        }

        private void OnCancel()
        {
            this.IsCancelled = true;
            this.Close();
        }

        private void OnAccept()
        {
            this.IsCancelled = false;
            this.Close();
        }


        public bool IsCancelled
        {
            get;
            set;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        
    }
}