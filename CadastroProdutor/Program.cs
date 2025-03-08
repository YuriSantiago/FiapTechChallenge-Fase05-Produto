using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Core.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration.GetSection("MassTransit")["Server"], "/", h =>
        {
            h.Username(configuration.GetSection("MassTransit")["User"]);
            h.Password(configuration.GetSection("MassTransit")["Password"]);
        });
    });
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>();
builder.Services.AddScoped<IRegiaoService, RegiaoService>();

// Adiciona a validação automática e adaptadores de cliente
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Registro dos validadores
builder.Services.AddValidatorsFromAssemblyContaining<ContatoRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegiaoRequestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
