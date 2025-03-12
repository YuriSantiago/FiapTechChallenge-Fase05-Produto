using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTestsV2
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

            Console.WriteLine($"ID São Paulo: {regiaoSP.Entity.Id}");
            Console.WriteLine($"ID Rio de Janeiro: {regiaoRJ.Entity.Id}");

            context.Contatos.Add(new Contato
            {
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                RegiaoId = regiaoSP.Entity.Id,
                Regiao = regiaoSP.Entity
            });

            context.Contatos.Add(new Contato
            {
                Nome = "Yago",
                Telefone = "999999999",
                Email = "yago@email.com",
                RegiaoId = regiaoRJ.Entity.Id,
                Regiao = regiaoRJ.Entity
            });

            await context.SaveChangesAsync();

            // Logar os IDs dos contatos para verificação
            var contatoYuri = context.Contatos.FirstOrDefault(c => c.Nome == "Yuri");
            var contatoYago = context.Contatos.FirstOrDefault(c => c.Nome == "Yago");

            Console.WriteLine($"ID Yuri: {contatoYuri?.Id}");
            Console.WriteLine($"ID Yago: {contatoYago?.Id}");
        }

    }
}
