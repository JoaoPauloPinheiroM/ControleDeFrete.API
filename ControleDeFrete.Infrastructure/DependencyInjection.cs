using ControleDeFrete.API.Infrastructure.Data;
using ControleDeFrete.API.Infrastructure.Data.Repositories;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeFrete.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database Context
        services.AddDbContext<ControleDeFreteContext>( options =>
            options.UseSqlServer( configuration.GetConnectionString( "DefaultConnection" ) ) );
        services.AddScoped<IMotoristaRepository , MotoristaRepository>();
        services.AddScoped<IVeiculoRepository , VeiculoRepository>();
        services.AddScoped<IFreteRepository , FreteRepository>();
        services.AddScoped<IClienteRepository , ClienteRepository>();
        // Unit of Work
        services.AddScoped<IUnitOfWork , UnitOfWork>();
        return services;
    }
}
