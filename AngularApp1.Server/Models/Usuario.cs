using System.Runtime.InteropServices;

namespace AngularApp1.Models
{
    public class Usuario : Persona
    {
        public string IdUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}