using System.Globalization;
using ControleDeFrete.WebAssembly.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace ControleDeFrete.WebAssembly.Services;

public sealed class FreteApi
{
    private const string BasePath = "api/fretes";
    private readonly ApiClient _api;

    public FreteApi( ApiClient api ) => _api = api;

    public Task<(List<FreteResponse>? Data, string? Error)> GetAllAsync()
        => _api.GetAsync<List<FreteResponse>>( BasePath );

    public Task<string?> CreateAsync( FreteCreate request )
    {
        var query = new Dictionary<string, string?>
        {
            [ "Codigo" ] = request.Codigo,
            [ "DocumentoCliente" ] = request.DocumentoCliente,
            [ "ValorFrete" ] = request.ValorFrete.ToString( CultureInfo.InvariantCulture ),
            [ "ValorDescarga" ] = request.ValorDescarga.ToString( CultureInfo.InvariantCulture ),
            [ "ValorPagoMotorista" ] = request.ValorPagoMotorista.ToString( CultureInfo.InvariantCulture ),
            [ "Logadouro" ] = request.Logadouro,
            [ "Cidade" ] = request.Cidade,
            [ "Estado" ] = request.Estado,
            [ "Latitude" ] = request.Latitude.ToString( CultureInfo.InvariantCulture ),
            [ "Longitude" ] = request.Longitude.ToString( CultureInfo.InvariantCulture )
        };

        var url = QueryHelpers.AddQueryString( BasePath , query );
        return _api.PostAsync( url );
    }

    public Task<string?> AssignMotoristaAsync( string codigo , string documentoMotorista )
    {
        var url = QueryHelpers.AddQueryString(
            $"{BasePath}/{Uri.EscapeDataString( codigo )}/alocar-motorista" ,
            new Dictionary<string, string?> { [ "documentoMotorista" ] = documentoMotorista }
        );
        return _api.PutAsync( url );
    }

    public Task<string?> AssignVeiculoAsync( string codigo , string placa )
    {
        var url = QueryHelpers.AddQueryString(
            $"{BasePath}/{Uri.EscapeDataString( codigo )}/alocar-veiculo" ,
            new Dictionary<string, string?> { [ "placa" ] = placa }
        );
        return _api.PutAsync( url );
    }

    public Task<string?> StartTransitAsync( string codigo , DateOnly dataInicio )
    {
        var url = QueryHelpers.AddQueryString(
            $"{BasePath}/{Uri.EscapeDataString( codigo )}/iniciar-transito" ,
            new Dictionary<string, string?>
            {
                [ "dataInicioTransito" ] = dataInicio.ToString( "yyyy-MM-dd" , CultureInfo.InvariantCulture )
            }
        );
        return _api.PutAsync( url );
    }

    public Task<string?> RegisterPaymentAsync( string codigo , DateOnly dataPagamento )
    {
        var url = QueryHelpers.AddQueryString(
            $"{BasePath}/{Uri.EscapeDataString( codigo )}/registrar-pagamento" ,
            new Dictionary<string, string?>
            {
                [ "dataPagamento" ] = dataPagamento.ToString( "yyyy-MM-dd" , CultureInfo.InvariantCulture )
            }
        );
        return _api.PutAsync( url );
    }

    public Task<string?> CancelAsync( string codigo )
        => _api.PutAsync( $"{BasePath}/{Uri.EscapeDataString( codigo )}/cancelar" );

    public Task<string?> ReopenAsync( string codigo )
        => _api.PutAsync( $"{BasePath}/{Uri.EscapeDataString( codigo )}/reabrir" );

    public Task<string?> AddEntregaAsync( string codigo , EntregaCreate request )
    {
        var url = QueryHelpers.AddQueryString(
            $"{BasePath}/{Uri.EscapeDataString( codigo )}/entregas" ,
            new Dictionary<string, string?>
            {
                [ "DocumentoCliente" ] = request.DocumentoCliente,
                [ "Logradouro" ] = request.Logradouro,
                [ "Cidade" ] = request.Cidade,
                [ "Estado" ] = request.Estado,
                [ "Latitude" ] = request.Latitude.ToString( CultureInfo.InvariantCulture ),
                [ "Longitude" ] = request.Longitude.ToString( CultureInfo.InvariantCulture ),
                [ "Observacoes" ] = request.Observacoes
            }
        );
        return _api.PostAsync( url );
    }

    public Task<string?> FinalizarEntregaAsync( string codigo , EntregaFinalizeRequest request )
    {
        var url = QueryHelpers.AddQueryString(
            $"{BasePath}/{Uri.EscapeDataString( codigo )}/entregas/{request.Sequencia}/finalizar" ,
            new Dictionary<string, string?>
            {
                [ "dataEntrega" ] = request.DataEntrega.ToString( "yyyy-MM-dd" , CultureInfo.InvariantCulture )
            }
        );
        return _api.PutAsync( url );
    }

    public Task<string?> RemoverEntregaAsync( string codigo , int sequencia )
        => _api.DeleteAsync( $"{BasePath}/{Uri.EscapeDataString( codigo )}/entregas/{sequencia}" );

    public Task<string?> DeleteAsync( string codigo )
        => _api.DeleteAsync( $"{BasePath}/{Uri.EscapeDataString( codigo )}" );
}
