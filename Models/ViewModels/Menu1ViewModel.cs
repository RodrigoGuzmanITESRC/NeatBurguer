namespace NeatBurguer.Models.ViewModels
{
    public class Menu1ViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripción { get; set; } = "";
        public double Precio { get; set; }

        public int OpcionAnterior { get; set; }
        public int OpcionSiguiente { get; set; }
    }
}
