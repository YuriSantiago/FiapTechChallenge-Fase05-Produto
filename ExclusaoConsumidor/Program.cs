using ExclusaoConsumidor;
using ExclusaoConsumidor.Eventos;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var queueContato = configuration.GetSection("MassTransit:Queues")["ContatoQueue"] ?? string.Empty;
var queueRegiao = configuration.GetSection("MassTransit:Queues")["RegiaoQueue"] ?? string.Empty;

builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>();
builder.Services.AddScoped<IRegiaoService, RegiaoService>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
}, ServiceLifetime.Scoped);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ContatoDeletado>();
    x.AddConsumer<RegiaoDeletada>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration.GetSection("MassTransit")["Server"], "/", h =>
        {
            h.Username(configuration.GetSection("MassTransit")["User"]);
            h.Password(configuration.GetSection("MassTransit")["Password"]);
        });

        cfg.ReceiveEndpoint(queueContato, e =>
        {
            e.ConfigureDefaultDeadLetterTransport();
            e.ConfigureConsumer<ContatoDeletado>(context);
        });

        cfg.ReceiveEndpoint(queueRegiao, e =>
        {
            e.ConfigureDefaultDeadLetterTransport();
            e.ConfigureConsumer<RegiaoDeletada>(context);
        });

        cfg.ConfigureEndpoints(context);
    });

});

var host = builder.Build();
host.Run();
