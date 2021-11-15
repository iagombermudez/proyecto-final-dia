namespace BusquedasUI.Core
{
    public class Cliente
    {
        private string Nombre { get; set; }

        public Cliente(string nombre)
        {
            this.Nombre = nombre;
        }

        public override string ToString()
        {
            return "Nombre: " + this.Nombre;
        }
    }
}