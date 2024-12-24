using FiapTechChallenge.Core.Entities;
using FiapTechChallenge.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace FiapTechChallenge.IntegrationTests
{
    //public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    //{
    //    protected override void ConfigureWebHost(IWebHostBuilder builder)
    //    {
    //        builder.ConfigureServices(services =>
    //        {
    //            // Substitui o ApplicationDbContext para usar o InMemoryDatabase em vez de SQL Server
    //            services.AddDbContext<ApplicationDbContext>(options =>
    //                options.UseInMemoryDatabase("InMemoryDbForTesting"));

    //            // Aqui você pode substituir outros serviços, se necessário.
    //        });
    //    }
    //}

    //public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    //{
    //    protected override void ConfigureWebHost(IWebHostBuilder builder)
    //    {
    //        base.ConfigureWebHost(builder);

    //        builder.ConfigureTestServices(services =>
    //        {
    //            // Remove o contexto de banco de dados existente
    //            var dbContext = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

    //            if (dbContext is not null)
    //                services.Remove(dbContext);

    //            // Configura o SQLite em memória
    //            services.AddDbContext<ApplicationDbContext>(options =>
    //            {
    //                options.UseSqlite("DataSource=:memory:");
    //            });

    //            // Certifique-se de inicializar o banco de dados antes de usá-lo
    //            using var serviceProvider = services.BuildServiceProvider();
    //            using var scope = serviceProvider.CreateScope();
    //            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //            db.Database.OpenConnection(); // Abre a conexão do SQLite em memória
    //            db.Database.EnsureCreated(); // Garante que o esquema está criado
    //        });

    //        builder.UseEnvironment("Development");
    //    }

    //}

    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();

                SeedDatabase(db).Wait();
            });

        }

        private static async Task SeedDatabase(ApplicationDbContext context)
        {
          
            context.Contatos.RemoveRange(context.Contatos);
            context.Regioes.RemoveRange(context.Regioes);

            await context.SaveChangesAsync();

            var regiaoSP = context.Regioes.Add(new Regiao
            {
                DDD = 11,
                Descricao = "São Paulo",
                DataInclusao = DateTime.UtcNow
            });

            var regiaoRJ = context.Regioes.Add(new Regiao
            {
                DDD = 21,
                Descricao = "Rio de Janeiro",
                DataInclusao = DateTime.UtcNow
            });

            context.Contatos.Add(new Contato
            {
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                RegiaoId = 1,
                Regiao = regiaoSP.Entity
            });

            context.Contatos.Add(new Contato
            {
                Nome = "Yago",
                Telefone = "999999999",
                Email = "yago@email.com",
                RegiaoId = 2,
                Regiao = regiaoRJ.Entity
            });

            await context.SaveChangesAsync();

            Console.WriteLine("Seeded Contatos: " + context.Contatos.Count());
            Console.WriteLine("Seeded Regioes: " + context.Regioes.Count());
        }
    }

}
