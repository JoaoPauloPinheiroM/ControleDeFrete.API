using ControleDeFrete.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeFrete.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices ( this IServiceCollection services , IConfiguration configuration )
    {
        services.AddDbContext<ControleDeFreteContext>( options =>
            options.UseSqlServer(
                configuration.GetConnectionString( "DefaultConnection" ) ) );

        services.Scan( scan => scan
                 .FromAssemblyOf<ControleDeFreteContext>()
                 .AddClasses( c =>
                     c.Where( t =>
                         t.Name.EndsWith( "Repository" ) ||
                         t.Name == "UnitOfWork" ) )
                 .AsImplementedInterfaces()
                 .WithScopedLifetime()
        );


        return services;
    }
}
