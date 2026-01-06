using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Clientes;

public class MudarStatusDoCliente : IMudarStatusDoCliente
{
    public Task<Result> ChangeStatusAsync ( int clienteId )
    {
        throw new NotImplementedException();
    }
}
