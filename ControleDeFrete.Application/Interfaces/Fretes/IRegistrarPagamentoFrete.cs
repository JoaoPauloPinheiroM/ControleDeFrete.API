using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Fretes;

public interface IRegistrarPagamentoFrete
{
    Task<Result> ExecuteAsync(string idFrete, DateOnly dataPagamento);
}
