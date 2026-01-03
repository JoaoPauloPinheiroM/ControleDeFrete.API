using ControleDeFrete.API.Domain.Entites;
using ControleDeFrete.API.Domain.Interfaces;
using ControleDeFrete.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleDeFrete.API.Infrastructure.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ControleDeFreteContext _context;
    public ClienteRepository ( ControleDeFreteContext context )
    {
        _context = context;
    }
    public async Task Adicionar ( Cliente cliente )
    {
        await _context.Clientes.AddAsync( cliente );
    }

    public async Task<IEnumerable<Cliente>> ObterClientes ( )
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();

    }

    public async Task<Cliente?> ObterPorDocumento ( string documento )
    {
        return await _context.Clientes
            .FirstOrDefaultAsync( c => c.Documento.Numero == documento );
    }

    public async Task<Cliente?> ObterPorId ( int id )
    {
        return await _context.Clientes.FindAsync( id );
    }
}
