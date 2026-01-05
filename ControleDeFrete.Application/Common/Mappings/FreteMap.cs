using ControleDeFrete.API.Application.Common.DTOS.Responses.Fretes;
using ControleDeFrete.Domain.Entites;

namespace ControleDeFrete.API.Application.Common.Mappings;

public static class FreteMap
{
    public static DetalhesFreteResponse ToResponse ( this Frete frete )
    {
        // Concatena as cidades das entregas separadas por " -> " 
        // Ex: "Cascavel -> Curitiba -> Paranaguá"
        var destinosConcatenados = string.Join( " -> " , frete.Entregas
            .OrderBy( e => e.Sequencia )
            .Select( e => e.Destino.Cidade ) );

        return new DetalhesFreteResponse(
            Codigo: frete.Codigo ,
            DataEmissao: frete.DataEmissao.Valor ,
            DataCarregamento: frete.DataCarregamento ,
            DataEntrega: frete.DataEntrega ,
            DataPagamento: frete.DataPagamento ,
            Valor: frete.Valor.Valor ,
            Descarga: frete.ValorDescarrego.Valor ,
            PagoMotorista: frete.ValorMotorista.Valor ,
            ValorTotal: frete.ValorTotal.Valor ,
            Status: frete.Status.ToString() ,
            MotoristaNome: "pendente" , // vai ser populado no handler
            VeiculoPlaca: "pendente" ,// vai ser populado no handler
            Origem: $"{frete.Origem.Cidade}-{frete.Origem.Estado}" ,
            Destinos: destinosConcatenados
        );
    }
}
