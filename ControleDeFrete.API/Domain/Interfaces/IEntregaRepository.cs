using ControleDeFrete.API.Domain.Entites;

namespace ControleDeFrete.API.Domain.Interfaces;

public interface IEntregaRepository
{
    public Task<Entrega?> ObterPorCodigoFrete ( string codigo );
    public Task<IEnumerable<Entrega>> ObterEntregas();
}
