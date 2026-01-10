using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Enums;
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
    public async Task AddAsync ( Cliente cliente )
    {
        await _context.Clientes.AddAsync( cliente );
    }

    public async Task<IEnumerable<Cliente>> GetBySatusAsync ( bool status )
    {
        return await _context.Clientes
            .AsNoTracking()
            .Where( c => c.Ativo == status ).ToListAsync();
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync ( )
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();

    }

    public async Task<Cliente?> GetByDocument ( CpfCnpj documento )
    {
        return await _context.Clientes
            .FirstOrDefaultAsync( c => c.Documento.Numero == documento.Numero );
    }

    public async Task<Cliente?> GetByIdAsync ( int id )
    {
        return await _context.Clientes.FindAsync( id );
    }
    public async Task<bool> GetFreteAtivo ( int idCliente )
    {
        return await _context.Fretes
            .AnyAsync( f => f.ClienteId == idCliente &&
                       f.Status != Status.Cancelado && 
                       f.Status != Status.Finalizado );
    }
}
