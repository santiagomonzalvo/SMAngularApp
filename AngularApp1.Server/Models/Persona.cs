using System.Runtime.InteropServices;
[assembly: ComVisible(false)]
[assembly: Guid("a1829a07-723d-49a5-a568-530531f1d648")]


namespace AngularApp1.Models
{
    public class Persona
    {
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
    }
}
