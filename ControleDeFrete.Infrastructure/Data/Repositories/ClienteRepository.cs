using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;
using ControleDeFrete.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleDeFrete.API.Infrastructure.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ControleDeFreteContext _context;
    public ClienteRepository ( ControleDeFreteContext context )
    {
        _context = context;
    }
    public async Task AdicionarAsync ( Cliente cliente )
    {
        await _context.Clientes.AddAsync( cliente );
    }

    public async Task<IEnumerable<Cliente>> ObterClientesAsync ( )
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();

    }

    public async Task<Cliente?> ObterPorDocumentoAsync ( string documento )
    {
        return await _context.Clientes
            .FirstOrDefaultAsync( c => c.Documento.Numero == documento );
    }

    public async Task<Cliente?> ObterPorIdAsync ( int id )
    {
        return await _context.Clientes.FindAsync( id );
    }
}
