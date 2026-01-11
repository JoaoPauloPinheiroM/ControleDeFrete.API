using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Fretes;

public interface IValidatorCliente
{
    Task<bool> ExistsClienteAsync ( string document);
}
