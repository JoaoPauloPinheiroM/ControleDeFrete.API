using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;
using ControleDeFrete.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleDeFrete.API.Infrastructure.Data.Repositories;

public class MotoristaRepository : IMotoristaRepository
{
    private readonly ControleDeFreteContext _context;
    public MotoristaRepository ( ControleDeFreteContext context )
    {
        _context = context;
    }
    public async Task AdicionarAsync ( Motorista motorista )
    {
        await _context.Motoristas.AddAsync( motorista );
    }

    public async Task<Motorista?> ObterDocumentoAsync ( CpfCnpj documento )
    {
        return await _context.Motoristas.AsNoTracking()
            .FirstOrDefaultAsync( m => m.Documento.Numero == documento.Numero );
    }

    public async Task<Motorista?> ObterPorIdAsync ( int motoristaId )
    {
        return await _context.Motoristas
            .FirstOrDefaultAsync( m => m.Id == motoristaId );
    }

    public async Task<IEnumerable<Motorista>> ObterTodosAsync ( )
    {
        return await _context.Motoristas.AsNoTracking()
            .ToListAsync();
    }
}
