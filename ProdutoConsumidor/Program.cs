using ProdutoConsumidor;
using ProdutoConsumidor.Eventos;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = builder.Configuration;

var queueCadastroProduto = configuration.GetSection("MassTransit:Queues")["ProdutoCadastroQueue"] ?? string.Empty;
var queueAtualizacaoProduto = configuration.GetSection("MassTransit:Queues")["ProdutoAtualizacaoQueue"] ?? string.Empty;
var queueExclusaoProduto = configuration.GetSection("MassTransit:Queues")["ProdutoExclusaoQueue"] ?? string.Empty;

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
        sql => sql.EnableRetryOnFailure());
}, ServiceLifetime.Scoped);

builder.Services.AddMassTransit(x =>
{

    x.AddConsumer<ProdutoCriado>();
    x.AddConsumer<ProdutoAtualizado>();
    x.AddConsumer<ProdutoDeletado>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration.GetSection("MassTransit")["Server"], "/", h =>
        {
            h.Username(configuration.GetSection("MassTransit")["User"]);
            h.Password(configuration.GetSection("MassTransit")["Password"]);
        });

        cfg.ReceiveEndpoint(queueCadastroProduto, e =>
        {
            e.ConfigureDefaultDeadLetterTransport();
            e.ConfigureConsumer<ProdutoCriado>(context);
        });

        cfg.ReceiveEndpoint(queueAtualizacaoProduto, e =>
        {
            e.ConfigureDefaultDeadLetterTransport();
            e.ConfigureConsumer<ProdutoAtualizado>(context);
        });

        cfg.ReceiveEndpoint(queueExclusaoProduto, e =>
        {
            e.ConfigureDefaultDeadLetterTransport();
            e.ConfigureConsumer<ProdutoDeletado>(context);
        });

        cfg.ConfigureEndpoints(context);
    });

   

});


var host = builder.Build();
host.Run();
