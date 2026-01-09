using ControleDeFrete.API.Infrastructure.Data.Repositories;
using ControleDeFrete.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeFrete.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices ( this IServiceCollection services , IConfiguration configuration )
    {
        // Database Context
        services.AddDbContext<ControleDeFreteContext>( options =>
            options.UseSqlServer( configuration.GetConnectionString( "DefaultConnection" ) ) );



        //services.Scan( scan => scan
        //    .FromAssemblyOf<FreteRepository>() // Escaneia o assembly onde os repositórios estão
        //    .AddClasses( classes => classes.Where( type => type.Name.EndsWith( "Repository" ) || type.Name == "UnitOfWork" ) )
        //    .AsImplementedInterfaces()
        //    .WithScopedLifetime() );


        services.Scan( scan => scan
            .FromAssembliesOf( typeof( ControleDeFreteContext ) )
            .AddClasses( classes => classes.Where( t => t.Name.EndsWith( "Repository" ) || t.Name == "UnitOfWork" ) )
            .AsImplementedInterfaces()
            .WithScopedLifetime() );


        return services;
    }
}
