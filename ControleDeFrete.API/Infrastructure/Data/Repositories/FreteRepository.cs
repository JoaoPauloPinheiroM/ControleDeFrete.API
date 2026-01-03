using ControleDeFrete.API.Domain.Entites;
using ControleDeFrete.API.Domain.Enums;
using ControleDeFrete.API.Domain.Interfaces;
using ControleDeFrete.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleDeFrete.API.Infrastructure.Data.Repositories;

public class FreteRepository : IFreteRepository
{
    private readonly ControleDeFreteContext _context;
    public FreteRepository ( ControleDeFreteContext context )
    {
        _context = context;
    }
    public async Task AdicionarAsync ( Frete frete )
    {
        await _context.Fretes.AddAsync ( frete );   
    }

    public async Task<Frete?> ObterFretePorCodigoAsync ( string codigo )
    {
       return await _context.Fretes.Include( f => f.Entregas )
            .FirstOrDefaultAsync ( f => f.Codigo == codigo );
    }

    public async Task<Frete?> ObterPorIdAsync ( int freteId )
    {
        return await _context.Fretes.Include( f => f.Entregas )
            .FirstOrDefaultAsync ( f => f.Id == freteId );
    }


    public async Task<IEnumerable<Frete>> ObterTodosAsync ( )
    {
        return await _context.Fretes
            .AsNoTracking()
            .Include( f => f.Entregas )
            .ToListAsync();
          
    }

    public async  Task<bool> RemoverFreteAsync ( Frete frete )
    {
        if(frete.PodeSerRemovido())
        {
             _context.Fretes.Remove ( frete );
            return true;
        }
        return false;

    }
    public async Task<bool> MotoristaPossuiFreteAtivoAsync ( int motoristaId )
    {
        
        return await _context.Fretes
            .AnyAsync( f => f.MotoristaId == motoristaId &&
                          (f.Status == Status.Pendente || f.Status == Status.EmTransito) );
    }
}
