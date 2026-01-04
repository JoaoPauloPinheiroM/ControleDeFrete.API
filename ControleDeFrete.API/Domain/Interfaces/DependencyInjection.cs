using ControleDeFrete.API.Infrastructure.Data;
using ControleDeFrete.API.Infrastructure.Data.Repositories;

namespace ControleDeFrete.API.Domain.Interfaces;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainInterfaces ( this IServiceCollection services )
    {
        // Repositories
        services.AddScoped<IMotoristaRepository, MotoristaRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IFreteRepository, FreteRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
