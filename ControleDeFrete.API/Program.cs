using ControleDeFrete.Infrastructure;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors( options =>
{
    options.AddPolicy( "AllowLocalWasm" , policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7142" ,
                "http://localhost:5242" ,
                "https://localhost:5001" ,
                "http://localhost:5000" ,
                "https://localhost:7246"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    } );
} );

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
        t.Name.StartsWith( "Excluir" ) ||
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

app.UseCors( "AllowLocalWasm" );

app.UseAuthorization();

app.MapControllers();

app.Run();






