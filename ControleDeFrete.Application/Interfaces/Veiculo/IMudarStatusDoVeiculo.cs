using ControleDeFrete.API.Application.Common.DTOS.Requests.Motoristas;
using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Veiculos;

public interface IMudarStatusDoVeiculo
{
    Task<Result> Execute ( string documento );
}
