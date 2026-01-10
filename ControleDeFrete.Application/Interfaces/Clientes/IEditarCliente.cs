using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Clientes;

public interface IEditarCliente
{
    Task<Result> Execute(string documento, string? Nome, string? novoDocumento, string? Logadouro, string? Cidade, string? Estado);
}
