using ControleDeFrete.API.Application.Services.Write;
using Microsoft.EntityFrameworkCore;
using ControleDeFrete.Infrastructure;
var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();
builder.Services.AddInfrastructureServices( builder.Configuration );
builder.Services.AddScoped<FreteWriteAppServices>();
builder.Services.AddScoped<MotoristaWriteAppServices>();
builder.Services.AddScoped<VeiculoWriteAppServices>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();






