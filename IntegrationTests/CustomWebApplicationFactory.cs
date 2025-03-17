using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
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

                db.Database.EnsureDeleted();  
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

            await context.SaveChangesAsync();

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
        }

    }
}
