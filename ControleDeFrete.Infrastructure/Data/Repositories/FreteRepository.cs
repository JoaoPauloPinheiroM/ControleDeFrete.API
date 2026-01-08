using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Enums;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleDeFrete.API.Infrastructure.Data.Repositories;

public class FreteRepository : IFreteRepository
{
    private readonly ControleDeFreteContext _context;
    public FreteRepository ( ControleDeFreteContext context )
    {
        _context = context;
    }
    public async Task AddAsync ( Frete frete )
    {
        await _context.Fretes.AddAsync( frete );
    }

    public async Task<Frete?> GetByCodigoAsync ( string codigo )
    {
        return await _context.Fretes.Include( f => f.Entregas )
             .FirstOrDefaultAsync( f => f.Codigo == codigo );
    }

    public async Task<Frete?> GetByIdAsync ( int freteId )
    {
        return await _context.Fretes.Include( f => f.Entregas )
            .FirstOrDefaultAsync( f => f.Id == freteId );
    }


    public async Task<IEnumerable<Frete>> GetAllAsync ( )
    {
        return await _context.Fretes
            .AsNoTracking()
            .Include( f => f.Entregas )
            .ToListAsync();

    }

    public async Task<bool> RemoveFreteAsync ( Frete frete )
    {
        if (frete.PodeSerRemovido())
        {
            _context.Fretes.Remove( frete );
            return true;
        }
        return false;

    }

    public async Task<IEnumerable<Frete>>  GetByClienteIdAsync (int idCliente)
    {
        return await _context.Fretes
            .AsNoTracking()
            .Include( f => f.Entregas )
            .Where( f => f.ClienteId == idCliente )
            .ToListAsync();
    }

    public async Task<IEnumerable<Frete>> GetByMotoristaIdAsync (int idMotorista)
    {
        return await _context.Fretes
            .AsNoTracking()
            .Include( f => f.Entregas )
            .Where( f => f.MotoristaId == idMotorista )
            .ToListAsync();
    }

    public async Task<IEnumerable<Frete>> GetbyStatusAsync (Status status)
    {
        return await _context.Fretes
            .AsNoTracking()
            .Include( f => f.Entregas )
            .Where( f => f.Status == status )
            .ToListAsync();
    }

}
