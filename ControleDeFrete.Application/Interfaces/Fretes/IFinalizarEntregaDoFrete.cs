using ControleDeFrete.API.Application.Common.Result;

namespace ControleDeFrete.Application.Interfaces.Fretes;

public interface IFinalizarEntregaDoFrete
{
    Task<Result> ExecuteAsync ( string codigoFrete, int sequencia , DateOnly dataEntrega );
}
