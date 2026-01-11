using ControleDeFrete.WebAssembly.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace ControleDeFrete.WebAssembly.Services;

public sealed class VeiculoApi
{
    private const string BasePath = "api/veiculos";
    private readonly ApiClient _api;

    public VeiculoApi( ApiClient api ) => _api = api;

    public Task<(List<VeiculoResponse>? Data, string? Error)> GetAllAsync()
        => _api.GetAsync<List<VeiculoResponse>>( BasePath );

    public Task<string?> CreateAsync( VeiculoCreate request )
        => _api.PostJsonAsync( BasePath , request );

    public Task<string?> EditAsync( string placa , VeiculoUpdate update )
    {
        var query = new Dictionary<string, string?>
        {
            [ "modelo" ] = update.Modelo,
            [ "marca" ] = update.Marca,
            [ "anoDeFabricacao" ] = update.AnoDeFabricacao?.ToString(),
            [ "novoPlaca" ] = update.NovoPlaca
        };

        var url = QueryHelpers.AddQueryString( $"{BasePath}/{Uri.EscapeDataString( placa )}" , query );
        return _api.PutAsync( url );
    }

    public Task<string?> ToggleStatusAsync( string placa )
        => _api.PutAsync( $"{BasePath}/{Uri.EscapeDataString( placa )}/mudar-status" );
}
