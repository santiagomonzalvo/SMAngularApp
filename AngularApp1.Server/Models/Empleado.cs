using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace AngularApp1.Models
{
    public class Empleado : Persona
    {
        [Key]
        public int IdEmpleado { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public int Antiguedad { get; set; }

        public Empleado(int idEmpleado, string cargo, decimal salario, int antiguedad)
        {
            IdEmpleado = idEmpleado;
            Cargo = cargo;
            Salario = salario;
            Antiguedad = antiguedad;
        }

        // Método para filtrar una lista de empleados por cargo
        public static IEnumerable<Empleado> FiltrarPorCargo(IEnumerable<Empleado> listaEmpleados, string cargo)
        {
            return listaEmpleados.Where(e => e.Cargo == cargo);
        }

    }
}
