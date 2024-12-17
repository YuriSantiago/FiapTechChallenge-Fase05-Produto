using FiapTechChallenge.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
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

    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            //Program.IsFromTest = true;

            builder.ConfigureTestServices(services =>
            {
                var dbContext = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                services.Remove(dbContext);

                var dbConnection = services.SingleOrDefault(x => x.ServiceType == typeof(DbConnection));
                services.Remove(dbConnection);

                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();
                    return connection;
                });

                services.AddDbContext<ApplicationDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlServer(connection);
                });

            });

            builder.UseEnvironment("Development");
        }
    }
}
