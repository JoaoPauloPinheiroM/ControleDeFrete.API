using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.ValueObjects;
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

        builder.Property<CpfCnpj>( c => c.Documento )
                        .HasConversion(
                            v => v.Numero ,
                            v => CpfCnpj.Create( v ).Value!
                        )
                        .HasColumnName( "Documento" )
                        .HasMaxLength( 14 );

        // Mapeamento de Localizacao
        builder.OwnsOne( c => c.Endereco , l =>
        {
            // Ajuda o compilador a entender que 'l' é um OwnedNavigationBuilder
            l.Property( x => x.Logradouro ).HasColumnName( "Logradouro" ).HasMaxLength( 200 );
            l.Property( x => x.Cidade ).HasColumnName( "Cidade" ).HasMaxLength( 100 );
            l.Property( x => x.Estado ).HasColumnName( "Estado" ).HasMaxLength( 2 );
            l.Property( x => x.Latitude ).HasColumnName( "Latitude" );
            l.Property( x => x.Longitude ).HasColumnName( "Longitude" );
        } );
    }
}
