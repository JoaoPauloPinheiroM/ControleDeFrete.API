using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Domain.Interfaces;

public interface IClienteRepository
{
    public Task<Cliente?> GetByIdAsync ( int id );
    public Task<Cliente?> GetByDocument ( CpfCnpj documento );
    public Task<bool> GetFreteAtivo ( int idCliente );
    public Task<IEnumerable<Cliente>> GetAllAsync ( );
    public Task<IEnumerable<Cliente>> GetBySatusAsync ( bool status);
    public Task AddAsync ( Cliente cliente );
}
