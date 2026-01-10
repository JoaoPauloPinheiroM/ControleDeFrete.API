using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Veiculo;

public interface IEditarVeiculo
{
    Task<Result> Execute (string placa, string? modelo, string? marca, int? anoDeFabricacao, string? novoPlaca);
}
