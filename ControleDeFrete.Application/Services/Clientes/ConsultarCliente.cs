using ControleDeFrete.API.Application.Common.DTOS.Responses.Clientes;
using ControleDeFrete.Application.Interfaces.Clientes;
using ControleDeFrete.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Clientes;

public class ConsultarCliente : IConsultarCliente
{
    public Task<IEnumerable<DetalhesClienteResponse>> GetAllClienteAsync ( )
    {
        throw new NotImplementedException();
    }

    public Task<DetalhesClienteResponse?> GetByDocumentAsync ( string document )
    {
        throw new NotImplementedException();
    }

    public Task<DetalhesClienteResponse?> GetByIdAsync ( int clienteId )
    {
        throw new NotImplementedException();
    }

    public Task<DetalhesClienteResponse?> GetByStatusAsync ( Status status )
    {
        throw new NotImplementedException();
    }
}
