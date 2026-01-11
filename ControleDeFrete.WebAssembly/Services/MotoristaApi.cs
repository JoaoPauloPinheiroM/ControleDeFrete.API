using ControleDeFrete.WebAssembly.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace ControleDeFrete.WebAssembly.Services;

public sealed class MotoristaApi
{
    private const string BasePath = "api/motorista";
    private readonly ApiClient _api;

    public MotoristaApi( ApiClient api ) => _api = api;

    public Task<(List<MotoristaResponse>? Data, string? Error)> GetAllAsync()
        => _api.GetAsync<List<MotoristaResponse>>( BasePath );

    public Task<string?> CreateAsync( MotoristaCreate request )
    {
        var query = new Dictionary<string, string?>
        {
            [ "Nome" ] = request.Nome,
            [ "Documento" ] = request.Documento,
            [ "Cnh" ] = request.Cnh,
            [ "Logradouro" ] = request.Logradouro,
            [ "Cidade" ] = request.Cidade,
            [ "Estado" ] = request.Estado
        };

        var url = QueryHelpers.AddQueryString( BasePath , query );
        return _api.PostAsync( url );
    }

    public Task<string?> EditAsync( string documento , MotoristaUpdate update )
    {
        var query = new Dictionary<string, string?>
        {
            [ "nome" ] = update.Nome,
            [ "cnh" ] = update.Cnh,
            [ "novoDocumento" ] = update.NovoDocumento,
            [ "logradouro" ] = update.Logradouro,
            [ "cidade" ] = update.Cidade,
            [ "estado" ] = update.Estado
        };

        var url = QueryHelpers.AddQueryString( $"{BasePath}/{Uri.EscapeDataString( documento )}" , query );
        return _api.PutAsync( url );
    }

    public Task<string?> ToggleStatusAsync( string documento )
        => _api.PutAsync( $"{BasePath}/{Uri.EscapeDataString( documento )}/mudar-status" );
}
