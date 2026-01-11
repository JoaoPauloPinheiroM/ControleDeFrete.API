using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Infrastructure.Data;

namespace ControleDeFrete.API.Infrastructure.Data.Repositories;

public class EntregaRepository : IEntregaRepository
{
    private readonly ControleDeFreteContext _context;
    public EntregaRepository ( ControleDeFreteContext context )
    {
        _context = context;
    }
    public async Task<IEnumerable<Entrega>> ObterEntregas ( )
    {
        throw new NotImplementedException();
    }

    public Task<Entrega?> ObterPorCodigoFrete ( string codigo )
    {
        throw new NotImplementedException();
    }
}
