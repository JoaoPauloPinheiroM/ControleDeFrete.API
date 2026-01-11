using ControleDeFrete.API.Application.Common.DTOS.Requests.Clientes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Clientes;

public interface ICriarCliente
{
    Task<Result> CreateClienteAsync ( CreateClienteRequest cliente );
}
