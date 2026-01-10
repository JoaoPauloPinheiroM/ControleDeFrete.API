using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Enums;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
        await _context.Veiculos.AddAsync( veiculo );
    }

    public async Task<Veiculo?> ObterPorIdAsync ( int id )
    {
        return await _context.Veiculos.FirstOrDefaultAsync( v => v.Id == id );
    }

    public async Task<Veiculo?> ObterPorPlacaAsync ( string placa )
    {
        return await _context.Veiculos
            .FirstOrDefaultAsync( v => v.Placa.Valor == placa );
    }

    public async Task<IEnumerable<Veiculo>> ObterPorStatusAsync ( bool status )
    {
        return await _context.Veiculos
            .Where( v => v.Ativo == status )
            .ToListAsync();
    }

    public async Task<IEnumerable<Veiculo>> ObterVeiculosAsync ( )
    {
        return await _context.Veiculos.AsNoTracking().ToListAsync();
    }


    public async Task<bool> VeiculoPossuiFreteAtivoAsync ( int veiculoId )
    {
        return await _context.Fretes
            .AnyAsync( f => f.VeiculoId == veiculoId &&
                       f.Status != Status.Cancelado &&
                       f.Status != Status.Finalizado );
    }
}
