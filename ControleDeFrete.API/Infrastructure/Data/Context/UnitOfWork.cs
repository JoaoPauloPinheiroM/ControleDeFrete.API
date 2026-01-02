using ControleDeFrete.API.Domain.Interfaces;

namespace ControleDeFrete.API.Infrastructure.Data.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly ControleDeFreteContext _context;

    public UnitOfWork ( ControleDeFreteContext context )
    {
        _context = context;
    }

    public async Task<bool> CommitAsync ( CancellationToken cancellationToken = default )
    {
        
        return await _context.SaveChangesAsync( cancellationToken ) > 0;
    }

    public void Dispose ( )
    {
        _context.Dispose();
    }
}
