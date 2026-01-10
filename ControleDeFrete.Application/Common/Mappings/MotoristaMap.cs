using ControleDeFrete.API.Application.Common.DTOS.Responses.Motoristas;
using ControleDeFrete.Domain.Entites;

namespace ControleDeFrete.API.Application.Common.Mappings;

public static class MotoristaMap
{
    public static DetalhesMotoristaResponse ToResponse ( this Motorista motorista )
    {
        return new DetalhesMotoristaResponse
        (
            motorista.Nome ,
            motorista.Documento.Numero ,
            motorista.Cnh ,
            $"{motorista.Endereco.Logradouro}, {motorista.Endereco.Cidade}-{motorista.Endereco.Estado}" ,
            motorista.Ativo
        );
    }
}
