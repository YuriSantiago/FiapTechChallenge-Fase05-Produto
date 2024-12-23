using FiapTechChallenge.Core.Entities;
using FiapTechChallenge.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

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
        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    base.ConfigureWebHost(builder);

        //    builder.ConfigureTestServices(async services =>
        //    {
        //        // Remove o DbContext existente
        //        var dbContext = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
        //        services.Remove(dbContext);

        //        // Remove a conexão existente
        //        var dbConnection = services.SingleOrDefault(x => x.ServiceType == typeof(DbConnection));
        //        services.Remove(dbConnection);

        //        // Configura a conexão SQLite em memória
        //        services.AddSingleton<DbConnection>(container =>
        //        {
        //            var connection = new SqliteConnection("DataSource=:memory:");
        //            connection.Open();
        //            return connection;
        //        });

        //        services.AddDbContext<ApplicationDbContext>((container, options) =>
        //        {
        //            var connection = container.GetRequiredService<DbConnection>();
        //            options.UseSqlite(connection);
        //        });

        //        // Garante que o banco de dados seja seedado
        //        using var scope = services.BuildServiceProvider().CreateScope();
        //        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        //        await context.Database.EnsureCreatedAsync(); // Cria o esquema do banco de dados
        //        await SeedDatabase(context); // Executa o Seed
        //    });

        //    builder.UseEnvironment("Development");
        //}

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove o contexto existente
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Adiciona um contexto SQLite em memória
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite("DataSource=:memory:");
                });

                // Configura o banco de dados
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.OpenConnection(); // Abre a conexão
                db.Database.EnsureCreated(); // Cria o esquema do banco

                // Seed dos dados
                SeedDatabase(db).Wait();
            });
        }

        private async Task SeedDatabase(ApplicationDbContext context)
        {
            // Truncate ou limpa os dados da tabela
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Regiao");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name='Regiao';");

            // Insere os dados iniciais
            context.Regioes.Add(new Regiao
            {
                DDD = 11,
                Descricao = "São Paulo",
                DataInclusao = DateTime.UtcNow
            });

            context.Regioes.Add(new Regiao
            {
                DDD = 21,
                Descricao = "Rio de Janeiro",
                DataInclusao = DateTime.UtcNow
            });

            await context.SaveChangesAsync();
        }
    }

}
