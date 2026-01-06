using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Motoristas;

public interface IMudarStatusDoMotorista
{
    Task<Result> Execute(int motoristaId);
}
