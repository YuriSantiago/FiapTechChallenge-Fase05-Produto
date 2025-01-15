using FiapTechChallenge.Core.Interfaces.Repositories;
using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Services;
using FiapTechChallenge.Core.Validators;
using FiapTechChallenge.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; 
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        x.IncludeXmlComments(xmlPath);

    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>();
builder.Services.AddScoped<IRegiaoService, RegiaoService>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();

// Adiciona a validação automática e adaptadores de cliente
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Registro dos validadores
builder.Services.AddValidatorsFromAssemblyContaining<ContatoRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ContatoUpdateRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegiaoRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegiaoUpdateRequestValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMetricServer();
app.UseHttpMetrics();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }