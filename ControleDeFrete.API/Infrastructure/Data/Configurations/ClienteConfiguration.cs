using ControleDeFrete.API.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeFrete.API.Infrastructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure ( EntityTypeBuilder<Cliente> builder )
    {
        builder.ToTable( "Clientes" );
        builder.HasKey( c => c.Id );

        builder.Property( c => c.Nome ).IsRequired().HasMaxLength( 150 );

        // Mapeamento de CpfCnpj
        builder.ComplexProperty( c => c.Documento , d =>
        {
            d.Property( x => x.Numero )
             .HasColumnName( "Documento" )
             .HasMaxLength( 14 );
        } );

        // Mapeamento de Localizacao
        builder.ComplexProperty( c => c.Endereco , l =>
        {
            l.Property( x => x.Logradouro ).HasColumnName( "Logradouro" ).HasMaxLength( 200 );
            l.Property( x => x.Cidade ).HasColumnName( "Cidade" ).HasMaxLength( 100 );
            l.Property( x => x.Estado ).HasColumnName( "Estado" ).HasMaxLength( 2 );
        } );
    }
}
