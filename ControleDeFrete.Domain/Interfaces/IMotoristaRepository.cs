using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Enums;
using ControleDeFrete.Domain.ValueObjects;


namespace ControleDeFrete.Domain.Interfaces;

public interface IMotoristaRepository
{
    public Task<Motorista?> ObterPorIdAsync ( int motoristaId );
    public Task<IEnumerable<Motorista>> ObterTodosAsync ( );
    public Task<Motorista?> ObterPorcumentoAsync ( string documento );
    public  Task<bool> MotoristaPossuiFreteAtivoAsync ( int motoristaId );
    public Task AdicionarAsync ( Motorista motorista );

}
