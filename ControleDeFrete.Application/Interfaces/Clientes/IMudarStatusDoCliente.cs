using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Clientes;

public interface IMudarStatusDoCliente
{
    Task<Result> ChangeStatusAsync(string documentoCliente);
}
