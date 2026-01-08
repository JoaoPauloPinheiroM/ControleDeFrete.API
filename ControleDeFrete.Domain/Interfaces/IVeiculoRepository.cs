using ControleDeFrete.Domain.Entites;

namespace ControleDeFrete.Domain.Interfaces;

public interface IVeiculoRepository
{
    public Task<Veiculo?> ObterPorIdAsync ( int id );
    public Task<Veiculo?> ObterPorPlacaAsync ( string placa );
    public Task<IEnumerable<Veiculo>> ObterVeiculosAsync();
    public Task<IEnumerable<Veiculo>> ObterPorStatusAsync(bool status);
    public Task<bool> VeiculoPossuiFreteAtivoAsync ( int veiculoId );
    public Task AdicionarAsync ( Veiculo veiculo );
}
