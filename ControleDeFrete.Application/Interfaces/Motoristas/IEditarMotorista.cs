using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Motoristas;

public interface IEditarMotorista
{
    Task<Result> Execute (string docuemento, string? nome, string? cnh, string? novoDoc, string? logadouro, string? cidade, string? estado);
}
