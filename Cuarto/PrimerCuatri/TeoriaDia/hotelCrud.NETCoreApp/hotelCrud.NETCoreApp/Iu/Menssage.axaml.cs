using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using hotel;
using hotel.IO;

namespace hotelCrud.NETCoreApp
{
    public partial class Message : Window
    {

        private bool confirmacion = false;

        public Message(string mensaje, bool confirmacion) : this()
        {
            var texBlock = this.FindControl<TextBlock>("AboutText");
            texBlock.Text = mensaje;
            this.confirmacion = confirmacion;

            var boton = this.FindControl<Button>("salirAyuda");
            if (confirmacion)
            {
                boton.IsVisible = false;
                var btAceptar = this.FindControl<Button>("btAceptar");
                var btCancelar = this.FindControl<Button>("btCancelar");
                btAceptar.IsVisible = true;
                btCancelar.IsVisible = true;
                btAceptar.Click += (_, _) => this.Aceptar();
                btCancelar.Click += (_, _) => this.Cancelar();
            }
            else { boton.Click += (_, _) => this.Close();}
        }

        public Message()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

        }
        private void Aceptar() {  this.Close();  }
        private void Cancelar() { this.Close();  }
        
        private void InitializeComponent()
            {
                AvaloniaXamlLoader.Load(this);
            }
    }
}