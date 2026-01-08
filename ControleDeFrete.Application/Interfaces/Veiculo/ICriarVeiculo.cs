using ControleDeFrete.API.Application.Common.DTOS.Requests.Motoristas;
using ControleDeFrete.API.Application.Common.DTOS.Requests.Veiculos;
using ControleDeFrete.API.Application.Common.Result;

namespace ControleDeFrete.Application.Interfaces.Veiculos;

public interface ICriarVeiculo
{
    Task<Result> Execute ( CreateVeiculoRequest veiculo );
}
