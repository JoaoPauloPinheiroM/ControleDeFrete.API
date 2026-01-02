using ControleDeFrete.API.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ControleDeFrete.API.Infrastructure.Data.Context;

public class ControleDeFretreContext : DbContext
{
    public ControleDeFretreContext ( DbContextOptions<ControleDeFretreContext> options ) : base( options ) { }

    DbSet<Frete> Fretes => Set<Frete>();
    DbSet<Motorista> Motoristas => Set<Motorista>();
    DbSet<Veiculo> Veiculos => Set<Veiculo>();
    DbSet<Cliente> Clientes => Set<Cliente>();
    protected override void OnModelCreating ( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );
        modelBuilder.ApplyConfigurationsFromAssembly( typeof( ControleDeFretreContext ).Assembly );
    }
}
