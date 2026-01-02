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
        builder.ComplexProperty( f => f.Valor );
        builder.ComplexProperty( f => f.ValorDescarrego );
        builder.ComplexProperty( f => f.ValorMotorista );
        builder.ComplexProperty( f => f.ValorTotal );
        builder.ComplexProperty( f => f.Origem );


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
