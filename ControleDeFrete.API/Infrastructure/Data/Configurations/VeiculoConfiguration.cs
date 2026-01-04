using ControleDeFrete.API.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeFrete.API.Infrastructure.Data.Configurations;

public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure ( EntityTypeBuilder<Veiculo> builder )
    {
        builder.ToTable( "Veiculos" );
        builder.HasKey( v => v.Id );

        builder.ComplexProperty( v => v.Placa , p =>
        {
            p.Property( x => x.Valor )
             .HasColumnName( "Placa" )
             .HasMaxLength( 7 )
             .IsFixedLength();
        } );
        builder.Property( v => v.Modelo ).IsRequired().HasMaxLength( 100 );
        builder.Property( v => v.Marca ).IsRequired().HasMaxLength( 50 );
        builder.Property( v => v.AnoFabricacao ).IsRequired();
    }
}