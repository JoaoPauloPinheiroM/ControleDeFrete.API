using ControleDeFrete.API.Application.Common.DTOS.Responses.Clientes;
using ControleDeFrete.API.Application.Common.Mappings;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Clientes;
using ControleDeFrete.Domain.Enums;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Clientes;

public class ConsultarCliente : IConsultarCliente
{
    private readonly IClienteRepository _clienteRepository;
    public ConsultarCliente ( IClienteRepository clienteRepository )
    {
        _clienteRepository = clienteRepository;
    }


    public async  Task<IEnumerable<DetalhesClienteResponse>> GetAllClienteAsync ( )
    {
        var clientes = await  _clienteRepository.GetAllAsync( );
        if (clientes is null) return [];


        var clientesResponse = new List<DetalhesClienteResponse>( );

        foreach ( var cliente in clientes )
        {
            clientesResponse.Add( cliente.ToResponse() );
        }

        return clientesResponse;

    }

    public async  Task<DetalhesClienteResponse?> GetByDocumentAsync ( string document )
    {
       var documento = CpfCnpj.Create( document );
        if (documento.IsFailure) return null;

        var cliente = await _clienteRepository.GetByDocument( documento.Value );

        if (cliente is null) return null;

        return cliente.ToResponse( );
    }

    public async Task<DetalhesClienteResponse?> GetByIdAsync ( int clienteId )
    {
        var cliente = await  _clienteRepository.GetByIdAsync( clienteId );
        if (cliente is null) return null;

        return cliente.ToResponse( );

    }

    public async Task<IEnumerable<DetalhesClienteResponse>> GetByStatusAsync ( bool status )
    {
        var cliente = await _clienteRepository.GetBySatusAsync( status );
        if (cliente is null) return [];

        var clientesResponse = new List<DetalhesClienteResponse>( );
        foreach ( var c in cliente )
        {
            clientesResponse.Add( c.ToResponse() );
        }

        return clientesResponse;
    }
}
