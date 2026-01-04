using ControleDeFrete.API.Application.Common.DTOS.Responses.Motoristas;
using ControleDeFrete.API.Domain.Entites;

namespace ControleDeFrete.API.Application.Common.Mappings;

public static class MotoristaMap
{
    public static DetalhesMotoristaResponse ToResponse ( this Motorista motorista )
    {
        return new DetalhesMotoristaResponse
        (
            motorista.Nome ,
            motorista.Documento ,
            motorista.Cnh ,
            $"{motorista.Endereco.Logradouro}, {motorista.Endereco.Cidade}-{motorista.Endereco.Estado}" ,
            motorista.Ativo
        );
    }
}
