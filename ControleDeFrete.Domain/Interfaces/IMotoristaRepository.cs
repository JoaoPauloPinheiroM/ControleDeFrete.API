using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.ValueObjects;


namespace ControleDeFrete.Domain.Interfaces;

public interface IMotoristaRepository
{
    public Task<Motorista?> ObterPorIdAsync ( int motoristaId );
    public Task<IEnumerable<Motorista>> ObterTodosAsync ( );
    public Task<Motorista?> ObterDocumentoAsync ( CpfCnpj documento );
    public Task AdicionarAsync ( Motorista motorista );

}
