using FiapTechChallenge.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FiapTechChallenge.Infrastructure.Repositories
{
    public class ApplicationDbContext : DbContext
    {

        private readonly string? _connectionString;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        { }

        //public ApplicationDbContext()
        //{
        //    IConfiguration configuration = new ConfigurationBuilder()
        //         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //         .AddJsonFile("appsettings.json")
        //         .Build();

        //    _connectionString = configuration.GetConnectionString("ConnectionString");
        //}

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Regiao> Regioes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
