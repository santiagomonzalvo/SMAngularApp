using AngularApp1.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularApp1.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;


namespace AngularApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : Controller
    {
        private readonly AppDbContext _context;

        public EmpleadoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Empleado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            try
            {
                return await _context.Empleados.ToListAsync();
            }
            catch (Exception ex)
            {
                LogError(ex);
                TempData["Mensaje"] = "Ocurrió un error interno del servidor. Por favor, inténtalo de nuevo más tarde.";
                return StatusCode(500);
            }
        }

        // GET: api/Empleado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);

                if (empleado == null)
                {
                    return NotFound();
                }

                return empleado;
            }
            catch (Exception ex)
            {
                LogError(ex);
                TempData["Mensaje"] = "Ocurrió un error interno del servidor. Por favor, inténtalo de nuevo más tarde.";
                return StatusCode(500);
            }
        }

        // POST: api/Empleado
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        {
            try
            {
                _context.Empleados.Add(empleado);
                await _context.SaveChangesAsync();

                // Multithreading (se espera hasta que la tarea en el nuevo thread haya completado su ejecución antes de continuar)
                await Task.Run(async () =>
                {
                    Console.WriteLine("ENVIADO"); 
                                                  // Tarea asincrónica
                    await Task.Delay(1000);
                });

                return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.IdEmpleado }, empleado);
            }
            catch (Exception ex)
            {
                LogError(ex);
                TempData["Mensaje"] = "Ocurrió un error interno del servidor. Por favor, inténtalo de nuevo más tarde.";
                return StatusCode(500);
            }
        }


        // PUT: api/Empleado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, Empleado empleado)
        {
            try
            {
                if (id != empleado.IdEmpleado)
                {
                    return BadRequest();
                }

                _context.Entry(empleado).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                LogError(ex);
                TempData["Mensaje"] = "Ocurrió un error interno del servidor. Por favor, inténtalo de nuevo más tarde.";
                return StatusCode(500);
            }
        }

        /// METODO ASINCRONO
        // DELETE: api/Empleado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);
                if (empleado == null)
                {
                    return NotFound();
                }

                // Simula una operación de espera de 3 segundos antes de eliminar al empleado
                await Task.Delay(3000);

                _context.Empleados.Remove(empleado);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                LogError(ex);
                TempData["Mensaje"] = "Ocurrió un error interno del servidor. Por favor, inténtalo de nuevo más tarde.";
                return StatusCode(500);
            }
        }


        //// DELETE: api/Empleado/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEmpleado(int id)
        //{
        //    try
        //    {
        //        var empleado = await _context.Empleados.FindAsync(id);
        //        if (empleado == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Empleados.Remove(empleado);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(ex);
        //        return StatusCode(500);
        //    }
        //}

        // GET: api/Empleado/SalarioMayorA5000
        [HttpGet("SalarioMayorA5000")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleadosConSalarioMayorA5000()
        {
            try
            {
                var empleados = await _context.Empleados.Where(e => e.Salario > 5000).ToListAsync();
                return empleados;
            }
            catch (Exception ex)
            {
                LogError(ex);
                TempData["Mensaje"] = "Ocurrió un error interno del servidor. Por favor, inténtalo de nuevo más tarde.";
                return StatusCode(500);
            }
        }

        // GET: api/Empleado/AntiguedadMayorA25
        [HttpGet("AntiguedadMayorA25")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleadosConAntiguedadMayorA25()
        {
            try
            {
                var empleados = await _context.Empleados.Where(e => e.Antiguedad > 25).ToListAsync();
                return empleados;
            }
            catch (Exception ex)
            {
                LogError(ex);
                TempData["Mensaje"] = "Ocurrió un error interno del servidor. Por favor, inténtalo de nuevo más tarde.";
                return StatusCode(500);
            }
        }

        // GET: api/Empleado/OperacionAsincronica
        [HttpGet("OperacionAsincronica")]
        public async Task<ActionResult<string>> RealizarOperacionAsincronica()
        {
            try
            {
                
                await Task.Delay(5000);

                return Ok("Simulación de operación asincrónica de 5 segundos completada!");
            }
            catch (Exception ex)
            {
                LogError(ex);
                TempData["Mensaje"] = "Ocurrió un error interno del servidor. Por favor, inténtalo de nuevo más tarde.";
                return StatusCode(500);
            }
        }



        private void LogError(Exception ex)
        {
            string logFilePath = "error_log.txt";
            string logMessage = $"[{DateTime.Now}] {ex.Message}\n{ex.StackTrace}\n\n";

            try
            {
                // Abre el archivo de registro en modo de escritura o lo crea si no existe
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    // Escribe el mensaje de error en el archivo de registro
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception logEx)
            {
               Console.WriteLine($"Error al escribir en el archivo de registro: {logEx.Message}");
            }
        }
    }
}