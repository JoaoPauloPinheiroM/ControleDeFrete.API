using ControleDeFrete.API.Domain.Entites;
using ControleDeFrete.API.Domain.ValueObjects;

namespace ControleDeFrete.API.Domain.Interfaces;

public interface IClienteRepository
{
    public Task<Cliente?> ObterPorIdAsync ( int id );
    public Task<Cliente?> ObterPorDocumentoAsync ( CpfCnpj documento );
    public Task<IEnumerable<Cliente>> ObterClientesAsync ( );
    public Task AdicionarAsync ( Cliente cliente );
}
