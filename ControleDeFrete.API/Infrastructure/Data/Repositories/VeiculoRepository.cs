using ControleDeFrete.API.Domain.Entites;
using ControleDeFrete.API.Domain.Interfaces;
using ControleDeFrete.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace ControleDeFrete.API.Infrastructure.Data.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly ControleDeFreteContext _context;
    public VeiculoRepository ( ControleDeFreteContext context )
    {
        _context = context;
    }
    public async Task AdicionarAsync ( Veiculo veiculo )
    {
        await _context.Veiculos.AddAsync(veiculo);
    }

    public async Task<Veiculo?> ObterPorIdAsync ( int id )
    {
        return await _context.Veiculos.AsNoTracking().FirstOrDefaultAsync( v => v.Id == id);
    }

    public async Task<Veiculo?> ObterPorPlacaAsync ( string placa )
    {
        return await _context.Veiculos.AsNoTracking()
            .FirstOrDefaultAsync( v => v.Placa.Valor == placa);
    }

    public async Task<IEnumerable<Veiculo>> ObterVeiculosAsync ( )
    {
        return await _context.Veiculos.AsNoTracking().ToListAsync();
    }
}
