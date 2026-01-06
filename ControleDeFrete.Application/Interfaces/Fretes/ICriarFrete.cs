using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Common.Result;

namespace ControleDeFrete.Application.Interfaces.Fretes;

public interface ICriarFrete
{
    Task<Result> ExecuteAsync ( CreateFreteRequest frete );
}
