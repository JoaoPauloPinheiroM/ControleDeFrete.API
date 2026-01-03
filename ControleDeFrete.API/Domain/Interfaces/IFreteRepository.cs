using ControleDeFrete.API.Domain.Entites;

namespace ControleDeFrete.API.Domain.Interfaces;

public interface IFreteRepository
{
    public Task<Frete?> ObterPorIdAsync ( int freteId );
    public Task<IEnumerable<Frete>> ObterTodosAsync ( );
    public Task<Frete?> ObterFretePorCodigoAsync ( string codigo );
    public Task AdicionarAsync ( Frete frete );
    public Task<bool> RemoverFreteAsync ( Frete frete );
    public Task<bool> MotoristaPossuiFreteAtivoAsync ( int motoristaId );

}
