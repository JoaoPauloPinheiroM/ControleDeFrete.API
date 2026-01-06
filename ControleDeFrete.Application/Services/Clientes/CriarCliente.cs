using ControleDeFrete.API.Application.Common.DTOS.Requests.Clientes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Clientes;

public class CriarCliente : ICriarCliente
{
    public Task<Result> CreateClienteAsync ( CreateClienteRequest cliente )
    {
        throw new NotImplementedException();
    }
}
