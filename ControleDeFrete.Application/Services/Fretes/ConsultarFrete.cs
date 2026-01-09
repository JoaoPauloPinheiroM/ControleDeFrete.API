using ControleDeFrete.API.Application.Common.DTOS.Responses.Fretes;
using ControleDeFrete.API.Application.Common.Mappings;
using ControleDeFrete.Domain.Enums;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFretes.Application.Interfaces.Fretes;

namespace ControleDeFrete.Application.Services.Fretes;

public class ConsultarFrete : IConsultarFrete
{
    private readonly IFreteRepository _freteRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IVeiculoRepository _veiculoRepository;


    public ConsultarFrete (
        IFreteRepository freteRepository ,
        IClienteRepository clienteRepository ,
        IMotoristaRepository motoristaRepository ,
        IVeiculoRepository veiculoRepository )
    {
        _freteRepository = freteRepository;
        _clienteRepository = clienteRepository;
        _motoristaRepository = motoristaRepository;
        _veiculoRepository = veiculoRepository;
    }
    public async Task<IEnumerable<DetalhesFreteResponse>> GetAllAsync ( )
    {
        var frete = await _freteRepository.GetAllAsync();
        if (frete is null)
            return []; ;
        var response = new List<DetalhesFreteResponse>();



        foreach (var f in frete)
        {
            var fResponse = f.ToResponse();
            response.Add( fResponse );
        }

        return response;
    }

    public async Task<IEnumerable<DetalhesFreteResponse>> GetByClienteIdAsync ( string docCliente )
    {
        var clinte = await _clienteRepository.GetByDocument( docCliente );
        if (clinte is null)
            return [];

        var frete = await _freteRepository.GetByClienteIdAsync( clinte.Id );
        if (frete is null)
            return [];

        var response = new List<DetalhesFreteResponse>();
        foreach (var f in frete)
        {
            var fResponse = f.ToResponse();
            response.Add( fResponse );
        }
        return response;
    }

    public async Task<DetalhesFreteResponse?> GetByIdAsync ( int idFrete )
    {
        var frete = await _freteRepository.GetByIdAsync( idFrete );
        if (frete is null)
            return null;

        var response = frete.ToResponse();

        return response;
    }

    public async Task<IEnumerable<DetalhesFreteResponse>> GetByMotoristaIdAsync ( string docMotorista )
    {
        var motorista = await _motoristaRepository.ObterPorcumentoAsync( docMotorista );
        if (motorista is null)
            return [];
        var frete = await _freteRepository.GetByMotoristaIdAsync( motorista.Id );
        if (frete is null)
            return [];

        var response = new List<DetalhesFreteResponse>();
        foreach (var f in frete)
        {
            var fResponse = f.ToResponse();
            response.Add( fResponse );
        }
        return response;

    }

    public async Task<IEnumerable<DetalhesFreteResponse>> GetByStatusAsync ( Status status )
    {
        var frete = await _freteRepository.GetbyStatusAsync( status );
        if (frete is null)
            return [];
        var response = new List<DetalhesFreteResponse>();
        foreach (var f in frete)
        {
            var fResponse = f.ToResponse();
            response.Add( fResponse );
        }
        return response;
    }
}
