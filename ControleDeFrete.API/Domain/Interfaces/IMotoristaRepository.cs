using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.API.Domain.Entites;
using ControleDeFrete.API.Domain.ValueObjects;


namespace ControleDeFrete.API.Domain.Interfaces;

public interface IMotoristaRepository
{
    public Task<Motorista?> ObterPorIdAsync ( int motoristaId );
    public Task<IEnumerable<Motorista>> ObterTodosAsync ( );
    public Task<Motorista?> ObterDocumentoAsync ( CpfCnpj documento );
    public Task AdicionarAsync ( Motorista motorista );
    public Task AtualizarAsync ( Motorista motorista );

}
