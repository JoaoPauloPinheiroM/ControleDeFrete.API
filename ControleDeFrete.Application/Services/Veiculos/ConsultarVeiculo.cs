using ControleDeFrete.API.Application.Common.DTOS.Responses.Veiculos;
using ControleDeFrete.API.Application.Common.Mappings;
using ControleDeFrete.Application.Interfaces.Veiculos;
using ControleDeFrete.Domain.Interfaces;

namespace ControleDeFrete.Application.Services.Veiculos;

public class ConsultarVeiculo : IConsultarVeiculo
{

    private readonly IVeiculoRepository _veiculoRepository;
    public ConsultarVeiculo ( IVeiculoRepository veiculoRepository )
    {
        _veiculoRepository = veiculoRepository;
    }


    public async  Task<IEnumerable<DetalhesVeiculoResponse>> GetAllVeiculosAsync ( )
    {
        var veiculos = await _veiculoRepository.ObterVeiculosAsync();
        if (veiculos is null)
            return [];

        var veiculosResponse = new List<DetalhesVeiculoResponse>();

        foreach(var veiculo in veiculos)
        {
            var veiculoResponse = veiculo.ToResponse();
            veiculosResponse.Add(veiculoResponse);
        }
        return veiculosResponse;

    }

    public async Task<DetalhesVeiculoResponse?> GetByIdAsync ( int veiculoId )
    {
        var veiculo = await _veiculoRepository.ObterPorIdAsync( veiculoId );
        if ( veiculo is null )
            return null;
        return veiculo.ToResponse();
    }

    public async Task<DetalhesVeiculoResponse?> GetByPlacaAsync ( string placa )
    {
        var veiculo = await _veiculoRepository.ObterPorPlacaAsync( placa );
        if ( veiculo is null )
            return null;

        return veiculo.ToResponse();
    }

    public async Task<IEnumerable<DetalhesVeiculoResponse>> GetByStatusAsync ( bool status )
    {
        var veiculos = await  _veiculoRepository.ObterPorStatusAsync( status );
        if ( veiculos is null )
            return [];

        var veiculosResponse = new List<DetalhesVeiculoResponse>();

        foreach ( var veiculo in veiculos )
        {
            var veiculoResponse = veiculo.ToResponse();
            veiculosResponse.Add( veiculoResponse );
        }

        return veiculosResponse;
    }
}
