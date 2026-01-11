using ControleDeFrete.API.Application.Common.DTOS.Requests.Motoristas;
using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Motoristas;

public interface ICriarMotorista
{
    Task<Result> Execute (CreateMotoristaRequest motorista );
}
