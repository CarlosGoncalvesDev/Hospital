using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class UserDbContext : DbContext
    {
        private IConfiguration _configuration;

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        public UserDbContext(IConfiguration configuration, DbContextOptions options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var typeDatabase = _configuration["TypeDatabase"];
            var connectionString = _configuration.GetConnectionString(typeDatabase);

            if (typeDatabase == "SqlServer")
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
