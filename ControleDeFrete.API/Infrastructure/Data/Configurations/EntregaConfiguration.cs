using ControleDeFrete.API.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeFrete.API.Infrastructure.Data.Configurations;


// Joao.Mourao 01-01-2026: Configuração da entidade Entrega, por mais que não precise do Dbset, a configuração é necessária para mapear corretamente as propriedades e relacionamentos.
public class EntregaConfiguration : IEntityTypeConfiguration<Entrega>
{
    public void Configure ( EntityTypeBuilder<Entrega> builder )
    {
        builder.ToTable( "Entregas" );
        builder.HasKey( e => e.Id );

        builder.Property( e => e.Sequencia ).IsRequired();
        builder.Property( e => e.Observacoes ).HasMaxLength( 500 );
        builder.Property( e => e.DataEntrega ).IsRequired( false );

        // Mapeamento de Localizacao (Destino)
        builder.ComplexProperty( e => e.Destino , l =>
        {
            l.Property( x => x.Logradouro ).HasColumnName( "Destino_Logradouro" );
            l.Property( x => x.Cidade ).HasColumnName( "Destino_Cidade" );
            l.Property( x => x.Estado ).HasColumnName( "Destino_Estado" );
            l.Property( x => x.Latitude ).HasColumnName( "Destino_Latitude" );
            l.Property( x => x.Longitude ).HasColumnName( "Destino_Longitude" );
        } );

        // Configuração explícita da FK
        builder.HasOne<Frete>()
               .WithMany( f => f.Entregas )
               .HasForeignKey( e => e.FreteId )
               .OnDelete( DeleteBehavior.Cascade );
    }
}
