using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Fretes;

public interface IAlocarMotoristsaNoFrete
{
    Task<Result> ExecuteAsync (string codigoFrete, string documentMotorista);
}
