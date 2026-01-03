using ControleDeFrete.API.Domain.Entites;

namespace ControleDeFrete.API.Domain.Interfaces;

public interface IClienteRepository
{
    public Task<Cliente?> ObterPorId ( int id );
    public Task<Cliente?> ObterPorDocumento ( string documento );
    public Task<IEnumerable<Cliente>> ObterClientes();
    public Task Adicionar ( Cliente cliente );
}
