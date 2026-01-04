namespace ControleDeFrete.API.Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices ( this IServiceCollection services )
    {
        // Write Services
        services.AddScoped<Write.FreteWriteAppServices>();
        services.AddScoped<Write.MotoristaWriteAppServices>();
        services.AddScoped<Write.VeiculoWriteAppServices>();
        return services;
    }
}
