using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Enums;

namespace ControleDeFrete.Domain.Interfaces;

public interface IFreteRepository
{
    public Task<Frete?> GetByIdAsync ( int freteId );
    public Task<IEnumerable<Frete>> GetAllAsync ( );
    public Task<Frete?> GetByCodigoAsync ( string codigo );
    public Task AddAsync ( Frete frete );
    public Task<bool> RemoveFreteAsync ( Frete frete );
    public  Task<IEnumerable<Frete>> GetByClienteIdAsync ( int idCliente );
    public  Task<IEnumerable<Frete>> GetByMotoristaIdAsync ( int idMotorista );
    public  Task<IEnumerable<Frete>> GetbyStatusAsync ( Status status );

}
