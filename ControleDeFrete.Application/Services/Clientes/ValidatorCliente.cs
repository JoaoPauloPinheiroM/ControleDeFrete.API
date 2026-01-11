using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Clientes;

public class ValidatorCliente : IValidatorCliente
{
    public Task<bool> ExistsClienteAsync ( string document )
    {
        throw new NotImplementedException();
    }
}
