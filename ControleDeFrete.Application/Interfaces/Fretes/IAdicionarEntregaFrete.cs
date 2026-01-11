using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Fretes;

public interface IAdicionarEntregaFrete
{
    Task<Result> Execute ( string codigoFrete, CreateEntregaFreteRequest entrega );
}
