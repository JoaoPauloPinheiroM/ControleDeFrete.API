using System.Net.Http.Json;
using System.Text.Json;

namespace ControleDeFrete.WebAssembly.Services;

public sealed class ApiClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _http;

    public ApiClient( HttpClient http ) => _http = http;

    public async Task<(T? Data, string? Error)> GetAsync<T>( string url )
    {
        try
        {
            var data = await _http.GetFromJsonAsync<T>( url , JsonOptions );
            return ( data , null );
        }
        catch ( Exception ex )
        {
            return ( default , ex.Message );
        }
    }

    public async Task<string?> PostJsonAsync<T>( string url , T payload )
    {
        var response = await _http.PostAsJsonAsync( url , payload , JsonOptions );
        return await ReadErrorAsync( response );
    }

    public async Task<string?> PostAsync( string url )
    {
        var response = await _http.PostAsync( url , null );
        return await ReadErrorAsync( response );
    }

    public async Task<string?> PutAsync( string url )
    {
        var response = await _http.PutAsync( url , null );
        return await ReadErrorAsync( response );
    }

    public async Task<string?> DeleteAsync( string url )
    {
        var response = await _http.DeleteAsync( url );
        return await ReadErrorAsync( response );
    }

    private static async Task<string?> ReadErrorAsync( HttpResponseMessage response )
    {
        if ( response.IsSuccessStatusCode )
            return null;

        var content = await response.Content.ReadAsStringAsync();
        if ( string.IsNullOrWhiteSpace( content ) )
            return response.ReasonPhrase ?? "Erro na requisição";

        return content;
    }
}
