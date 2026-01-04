using ControleDeFrete.API.Domain.Entites;
using ControleDeFrete.API.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeFrete.API.Infrastructure.Data.Configurations;

public class FreteConfiguration : IEntityTypeConfiguration<Frete>
{
    public void Configure( EntityTypeBuilder<Frete> builder)
    {
        builder.ToTable("Fretes");
        builder.HasKey(f => f.Id);


        // Joao.mourao 01-01-2026:  O código do frete é obrigatório e deve ter no máximo 20 caracteres.
        builder.Property( f => f.Codigo )
            .IsRequired()
            .HasMaxLength( 20 );

        // Joao.mourao 01-01-2026: Mapeamento dos VOs como complexas 
        builder.Property( f => f.Valor ).HasConversion( v => v.Valor , v => Money.From( v ) );
        builder.Property( f => f.ValorDescarrego ).HasConversion( v => v.Valor , v => Money.From( v ) );
        builder.Property( f => f.ValorMotorista ).HasConversion( v => v.Valor , v => Money.From( v ) );
        builder.Ignore( f => f.ValorTotal );
        builder.ComplexProperty( f => f.Origem , l =>
        {
            l.Property( x => x.Logradouro ).HasColumnName( "Origem_Logradouro" ).HasMaxLength( 200 );
            l.Property( x => x.Cidade ).HasColumnName( "Origem_Cidade" ).HasMaxLength( 100 );
            l.Property( x => x.Estado ).HasColumnName( "Origem_Estado" ).HasMaxLength( 2 );
            l.Property( x => x.Latitude ).HasColumnName( "Origem_Latitude" );
            l.Property( x => x.Longitude ).HasColumnName( "Origem_Longitude" );
        } );


        // Joao.mourao 01-01-2026: Conversao das datas para DateOnly
        builder.Property(f => f.DataEmissao)
            .HasConversion( 
                fato => fato.Valor,
                date => DataFato.Create( date ).Value

            ).HasColumnName("DataEmissao")
            .IsRequired();
            
       // Joao.mourao 01-01-2026: Configuração das propriedades privadas
       builder.Navigation( f => f.Entregas )
            .HasField("_entregas")
            .UsePropertyAccessMode( PropertyAccessMode.Field );


      builder.HasMany(f => f.Entregas )
            .WithOne()
            .HasForeignKey(  e => e.FreteId)
            .OnDelete( DeleteBehavior.Cascade );

    }
}
