using ControleDeFrete.API.Application.Common.DTOS.Responses.Motoristas;
using ControleDeFrete.API.Application.Common.Mappings;
using ControleDeFrete.Application.Interfaces.Motoristas;
using ControleDeFrete.Domain.Enums;
using ControleDeFrete.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Motoristas;

public class ConsultarMotorista : IConsultarMotorista
{
    private readonly IMotoristaRepository _motoristaRepository;
    public ConsultarMotorista ( IMotoristaRepository motoristaRepository )
    {
        _motoristaRepository = motoristaRepository;
    }

    public async  Task<IEnumerable<DetalhesMotoristaResponse>> GetAllMotoristaAsync ( )
    {
        var motoristas = await  _motoristaRepository.ObterTodosAsync();
        if (motoristas is null)
            return [];

        var motoristaResponses = new List<DetalhesMotoristaResponse>();

        foreach (var motorista in motoristas)
        {
            var motoristaResponse = motorista.ToResponse();
            motoristaResponses.Add(motoristaResponse);
        }
        return motoristaResponses;
    }


    public async Task<DetalhesMotoristaResponse?> GetByDocumentAsync ( string documento )
    {
        var motorista = await _motoristaRepository.ObterPorcumentoAsync( documento );
        if (motorista is null)
            return null;

        return motorista.ToResponse();

    }

    public async  Task<DetalhesMotoristaResponse?> GetByIdAsync ( int motoristaId )
    {
        var motorista = await _motoristaRepository.ObterPorIdAsync( motoristaId );
        if (motorista is null)
            return null;
        return motorista.ToResponse();
    }

    public async Task<IEnumerable<DetalhesMotoristaResponse>> GetByStatusAsync ( bool status )
    {
        var motoristas = await  _motoristaRepository.GetByStatusAsync( status );
        if (motoristas is null)
            return [];

        var motoristaResponses = new List<DetalhesMotoristaResponse>();

        foreach (var motorista in motoristas)
        {
            var motoristaResponse = motorista.ToResponse();
            motoristaResponses.Add( motoristaResponse );
        }
        return motoristaResponses;
    }

}
