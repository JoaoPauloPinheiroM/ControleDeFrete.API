using ControleDeFrete.API.Domain.Entites;
using ControleDeFrete.API.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeFrete.API.Infrastructure.Data.Configurations;

public class MotoristaConfiguration : IEntityTypeConfiguration<Motorista>
{
    public void Configure ( EntityTypeBuilder<Motorista> builder )
    {
        builder.ToTable( "Motoristas" );
        builder.HasKey( m => m.Id );

        builder.Property( m => m.Nome ).IsRequired().HasMaxLength( 150 );
        builder.Property( m => m.Cnh ).IsRequired().HasMaxLength( 20 );

        // Value Objects
        builder.ComplexProperty( m => m.Documento );
        builder.ComplexProperty( m => m.Endereco );

        builder.Property( m => m.DataCadastro )
            .HasConversion( v => v.Valor , v => DataFato.Create( v ).Value );
    }
}
