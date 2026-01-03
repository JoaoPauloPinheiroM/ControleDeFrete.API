using ControleDeFrete.API.Domain.Interfaces;
using ControleDeFrete.API.Infrastructure.Data;
using ControleDeFrete.API.Infrastructure.Data.Context;
using ControleDeFrete.API.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
builder.Services.AddDbContext<ControleDeFreteContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );
builder.Services.AddScoped<IFreteRepository, FreteRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IEntregaRepository, EntregaRepository>();
builder.Services.AddScoped<IMotoristaRepository, MotoristaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
