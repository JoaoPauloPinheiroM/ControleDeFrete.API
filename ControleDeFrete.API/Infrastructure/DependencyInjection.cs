using ControleDeFrete.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleDeFrete.API.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database Context
        services.AddDbContext<ControleDeFreteContext>( options =>
            options.UseSqlServer( configuration.GetConnectionString( "DefaultConnection" ) ) );
        return services;
    }
}
