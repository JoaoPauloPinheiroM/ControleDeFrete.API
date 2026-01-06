using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Domain.Interfaces;

public interface IClienteRepository
{
    public Task<Cliente?> ObterPorIdAsync ( int id );
    public Task<Cliente?> ObterPorDocumentoAsync ( string documento );
    public Task<IEnumerable<Cliente>> ObterClientesAsync ( );
    public Task AdicionarAsync ( Cliente cliente );
}
