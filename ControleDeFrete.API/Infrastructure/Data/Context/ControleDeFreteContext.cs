using ControleDeFrete.API.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ControleDeFrete.API.Infrastructure.Data.Context;

public class ControleDeFreteContext : DbContext
{
    public ControleDeFreteContext ( DbContextOptions<ControleDeFreteContext> options )
        : base( options )
    {
    }

    public DbSet<Frete> Fretes => Set<Frete>();
    public DbSet<Motorista> Motoristas => Set<Motorista>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating ( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );
        modelBuilder.ApplyConfigurationsFromAssembly( typeof( ControleDeFreteContext ).Assembly );
    }
}
