using ControleDeFrete.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();

// Configuração do NSwag para gerar o documento OpenAPI (Swagger)
builder.Services.AddOpenApiDocument( config =>
{
    config.PostProcess = document =>
    {
        document.Info.Title = "Controle de Frete API";
        document.Info.Version = "v1";
        document.Info.Description = "API responsável pelo controle de fretes, motoristas, veículos e clientes.";
    };
} );

// Registra Infraestrutura (Repositorios e UoW via Scrutor definido )
builder.Services.AddInfrastructureServices( builder.Configuration );
builder.Services.Scan( scan => scan
    .FromAssemblies(
        ControleDeFrete.Application.AssemblyReference.Assembly
    )
    .AddClasses( classes => classes.Where( t =>
        t.Name.StartsWith( "Criar" ) ||
        t.Name.StartsWith( "Editar" ) ||
        t.Name.StartsWith( "Mudar" ) ||
        t.Name.StartsWith( "Consultar" ) ||
        t.Name.StartsWith( "Adicionar" ) ||
        t.Name.StartsWith( "Finalizar" ) ||
        t.Name.StartsWith( "Cancelar" ) ||
        t.Name.StartsWith( "Alocar" ) ||
        t.Name.StartsWith( "Registrar" ) ||
        t.Name.StartsWith( "Remover" ) ||
        t.Name.StartsWith( "Reabrir" ) ||
        t.Name.StartsWith( "Iniciar" )
    ) )
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

//Validator.... irei implementar la  na frente
builder.Services.Scan( scan => scan
    .FromAssemblies( ControleDeFrete.Application.AssemblyReference.Assembly )
    .AddClasses( classes => classes.Where( t =>
        t.Name.StartsWith( "Validator" )
    ) )
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);



var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    
    app.UseOpenApi();
    
    app.UseSwaggerUi();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();






