using FiapTechChallenge.Core.Interfaces.Repositories;
using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Services;
using FiapTechChallenge.Infrastructure.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString"));
        });

        services.AddScoped<IRegiaoRepository, RegiaoRepository>();
        services.AddScoped<IRegiaoService, RegiaoService>();
        services.AddScoped<IContatoRepository, ContatoRepository>();
        services.AddScoped<IContatoService, ContatoService>();
    })
    .Build();

host.Run();
