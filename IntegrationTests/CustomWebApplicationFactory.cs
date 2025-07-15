using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

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

                services.RemoveAll<IConfigureOptions<AuthenticationOptions>>();
                services.RemoveAll<IConfigureOptions<Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions>>();

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                services.AddAuthentication("TestScheme")
                  .AddScheme<AuthenticationSchemeOptions, JwtAuthHandlerSimulation>("TestScheme", options =>
                  {
                      options.TimeProvider = TimeProvider.System;
                  });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                SeedDatabase(db).Wait();
            });

        }

        private static async Task SeedDatabase(ApplicationDbContext context)
        {
            context.Produtos.RemoveRange(context.Produtos);
            context.Categorias.RemoveRange(context.Categorias);
           
            await context.SaveChangesAsync();

            var categoria = context.Categorias.Add(new Categoria
            {
                Descricao = "LANCHE",
                DataInclusao = DateTime.Now
            });

            await context.SaveChangesAsync();

            context.Produtos.Add(new Produto
            {
                DataInclusao = DateTime.Now,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1,
                Categoria = categoria.Entity
            });

            await context.SaveChangesAsync();
        }

    }
}