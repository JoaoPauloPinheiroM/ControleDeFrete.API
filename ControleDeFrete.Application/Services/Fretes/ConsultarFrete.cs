using ControleDeFrete.API.Application.Common.DTOS.Responses.Fretes;
using ControleDeFrete.API.Application.Common.Mappings;
using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Enums;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;
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
            return [];

        var response = new List<DetalhesFreteResponse>();

        foreach (var f in frete)
        {
            string motoristaNome = string.Empty;
            string veiculoPlaca = string.Empty;
            if (f.MotoristaId is int motoristaId)
            {
                var motorista = await _motoristaRepository.ObterPorIdAsync( motoristaId );
                motoristaNome = motorista?.Nome ?? "Pendente";
            }
            if (f.VeiculoId is int veiculoId)
            {
                var placa = await _veiculoRepository.ObterPorIdAsync( veiculoId );
                veiculoPlaca = placa?.Placa ?? "Pendente";
            }
            var fResponse = f.ToResponse() with
            {
                MotoristaNome = motoristaNome ,
                VeiculoPlaca = veiculoPlaca ,

            };

            response.Add( fResponse );
        }

        return response;
    }

    public async Task<IEnumerable<DetalhesFreteResponse>> GetByClienteIdAsync ( string docCliente )
    {
        var docResult = CpfCnpj.Create( docCliente );
        if (docResult.IsFailure)
            return [];

        var clinte = await _clienteRepository.GetByDocument( docResult.Value );
        if (clinte is null)
            return [];

        var frete = await _freteRepository.GetByClienteIdAsync( clinte.Id );
        if (frete is null)
            return [];

        var response = new List<DetalhesFreteResponse>();
        foreach (var f in frete)
        {
            string motoristaNome = string.Empty;
            string veiculoPlaca = string.Empty;
            if (f.MotoristaId is int motoristaId)
            {
                var motorista = await _motoristaRepository.ObterPorIdAsync( motoristaId );
                motoristaNome = motorista?.Nome ?? "Pendente";
            }
            if (f.VeiculoId is int veiculoId)
            {
                var placa = await _veiculoRepository.ObterPorIdAsync( veiculoId );
                veiculoPlaca = placa?.Placa ?? "Pendente";
            }
            var fResponse = f.ToResponse() with
            {
                MotoristaNome = motoristaNome ,
                VeiculoPlaca = veiculoPlaca ,

            };

            response.Add( fResponse );
        }
        return response;
    }

    public async Task<DetalhesFreteResponse?> GetByIdAsync ( int idFrete )
    {
        var frete = await _freteRepository.GetByIdAsync( idFrete );
        if (frete is null)
            return null;


        string motoristaNome = string.Empty;
        string veiculoPlaca = string.Empty;


        if (frete.MotoristaId is int motoristaId)
        {
            var motorista = await _motoristaRepository.ObterPorIdAsync( motoristaId );
            motoristaNome = motorista?.Nome ?? "Pendente";
        }
        if (frete.VeiculoId is int veiculoId)
        {
            var placa = await _veiculoRepository.ObterPorIdAsync( veiculoId );
            veiculoPlaca = placa?.Placa ?? "Pendente";
        }
        var response = frete.ToResponse() with
        {
            MotoristaNome = motoristaNome ,
            VeiculoPlaca = veiculoPlaca ,

        };

        return response;
    }
    public async Task<DetalhesFreteResponse?> GetByCodigoAsync ( string codigoFrete )
    {
        var frete = await _freteRepository.GetByCodigoAsync( codigoFrete );
        if (frete is null)
            return null;


        string motoristaNome = string.Empty;
        string veiculoPlaca = string.Empty;


        if (frete.MotoristaId is int motoristaId)
        {
            var motorista = await _motoristaRepository.ObterPorIdAsync( motoristaId );
            motoristaNome = motorista?.Nome ?? "Pendente";
        }
        if (frete.VeiculoId is int veiculoId)
        {
            var placa = await _veiculoRepository.ObterPorIdAsync( veiculoId );
            veiculoPlaca = placa?.Placa ?? "Pendente";
        }
        var response = frete.ToResponse() with
        {
            MotoristaNome = motoristaNome ,
            VeiculoPlaca = veiculoPlaca ,

        };

        return response;
    }

    public async Task<IEnumerable<DetalhesFreteResponse>> GetByMotoristaIdAsync ( string docMotorista )
    {
        var doc = CpfCnpj.Create( docMotorista );
        if (doc.IsFailure)
            return [];

        var motorista = await _motoristaRepository.ObterPorcumentoAsync( doc.Value );
        if (motorista is null)
            return [];
        var frete = await _freteRepository.GetByMotoristaIdAsync( motorista.Id );
        if (frete is null)
            return [];

        var response = new List<DetalhesFreteResponse>();
        foreach (var f in frete)
        {
            string motoristaNome = string.Empty;
            string veiculoPlaca = string.Empty;
            if (f.MotoristaId is int motoristaId)
            {
                var motoristaNomeResult = await _motoristaRepository.ObterPorIdAsync( motoristaId );
                motoristaNome = motoristaNomeResult?.Nome ?? "Pendente";
            }
            if (f.VeiculoId is int veiculoId)
            {
                var placa = await _veiculoRepository.ObterPorIdAsync( veiculoId );
                veiculoPlaca = placa?.Placa ?? "Pendente";
            }
            var fResponse = f.ToResponse() with
            {
                MotoristaNome = motoristaNome ,
                VeiculoPlaca = veiculoPlaca ,

            };

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
            string motoristaNome = string.Empty;
            string veiculoPlaca = string.Empty;
            if (f.MotoristaId is int motoristaId)
            {
                var motorista = await _motoristaRepository.ObterPorIdAsync( motoristaId );
                motoristaNome = motorista?.Nome ?? "Pendente";
            }
            if(f.VeiculoId is int veiculoId)
            {
                var placa = await _veiculoRepository.ObterPorIdAsync ( veiculoId );
                veiculoPlaca = placa?.Placa ?? "Pendente";
            }
            var fResponse = f.ToResponse() with
            {
                MotoristaNome = motoristaNome,
                VeiculoPlaca= veiculoPlaca,
                
            };

            response.Add( fResponse );
        }


        return response;
    }
}
