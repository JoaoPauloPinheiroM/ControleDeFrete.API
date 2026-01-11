using ControleDeFrete.Domain.Entites;

namespace ControleDeFrete.Domain.Interfaces;

public interface IEntregaRepository
{
    public Task<Entrega?> ObterPorCodigoFrete ( string codigo );
    public Task<IEnumerable<Entrega>> ObterEntregas();
}
