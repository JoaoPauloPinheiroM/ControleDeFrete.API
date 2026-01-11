using ControleDeFrete.WebAssembly.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace ControleDeFrete.WebAssembly.Services;

public sealed class ClienteApi
{
    private const string BasePath = "api/clientes";
    private readonly ApiClient _api;

    public ClienteApi( ApiClient api ) => _api = api;

    public Task<(List<ClienteResponse>? Data, string? Error)> GetAllAsync()
        => _api.GetAsync<List<ClienteResponse>>( BasePath );

    public Task<string?> CreateAsync( ClienteCreate request )
        => _api.PostJsonAsync( BasePath , request );

    public Task<string?> ToggleStatusAsync( string documento )
        => _api.PutAsync( $"{BasePath}/{Uri.EscapeDataString( documento )}/mudar-status" );

    public Task<string?> EditAsync( string documento , ClienteUpdate update )
    {
        var query = new Dictionary<string, string?>
        {
            [ "nome" ] = update.Nome,
            [ "novoDocumento" ] = update.NovoDocumento,
            [ "logradouro" ] = update.Logradouro,
            [ "cidade" ] = update.Cidade,
            [ "estado" ] = update.Estado
        };

        var url = QueryHelpers.AddQueryString( $"{BasePath}/{Uri.EscapeDataString( documento )}" , query );
        return _api.PutAsync( url );
    }
}
