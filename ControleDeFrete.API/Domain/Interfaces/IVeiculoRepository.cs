using ControleDeFrete.API.Domain.Entites;

namespace ControleDeFrete.API.Domain.Interfaces;

public interface IVeiculoRepository
{
    public Task<Veiculo?> ObterPorId ( int id );
    public Task<Veiculo?> ObterPorPlaca ( string placa );
    public Task<IEnumerable<Veiculo>> ObterVeiculos();
    public Task Adicionar ( Veiculo veiculo );
}
