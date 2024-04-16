using AngularApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace AngularApp1.DataAccess
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;

            // Desactivar migraciones autom�ticas y aplicar migraciones manualmente
            Database.Migrate();
        }

        public DbSet<Empleado> Empleados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 2, // n�mero m�ximo de intentos de reconexi�n
                        maxRetryDelay: TimeSpan.FromSeconds(30), // tiempo m�ximo de espera entre intentos de reconexi�n
                        errorNumbersToAdd: null); // n�meros de error espec�ficos a considerar para la resiliencia
                });
        }
    }
}
