using ControleDeFrete.API.Domain.Entites;

namespace ControleDeFrete.API.Domain.Interfaces;

public interface IVeiculoRepository
{
    public Task<Veiculo?> ObterPorIdAsync ( int id );
    public Task<Veiculo?> ObterPorPlacaAsync ( string placa );
    public Task<IEnumerable<Veiculo>> ObterVeiculosAsync();
    public Task AdicionarAsync ( Veiculo veiculo );
}
