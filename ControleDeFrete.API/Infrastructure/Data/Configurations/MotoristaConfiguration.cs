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
        builder.Property( m => m.Documento )
             .HasConversion(
                 v => v.Numero ,
                 v => CpfCnpj.Create( v ).Value
             )
             .HasColumnName( "Documento" )
             .HasMaxLength( 14 );

        builder.ComplexProperty( m => m.Endereco , l =>
        {
            l.Property( x => x.Logradouro ).HasColumnName( "Origem_Logradouro" ).HasMaxLength( 200 );
            l.Property( x => x.Cidade ).HasColumnName( "Origem_Cidade" ).HasMaxLength( 100 );
            l.Property( x => x.Estado ).HasColumnName( "Origem_Estado" ).HasMaxLength( 2 );
            l.Property( x => x.Latitude ).HasColumnName( "Origem_Latitude" );
            l.Property( x => x.Longitude ).HasColumnName( "Origem_Longitude" );
        } );

        builder.Property( m => m.DataCadastro )
            .HasConversion( v => v.Valor , v => DataFato.Create( v ).Value );
    }
}

