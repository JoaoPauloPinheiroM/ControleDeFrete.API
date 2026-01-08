using ControleDeFrete.API.Application.Common.Result;

namespace ControleDeFrete.Application.Interfaces.Motoristas;

public interface IMudarStatusDoMotorista
{
    Task<Result> Execute ( string docMotorista );
}
