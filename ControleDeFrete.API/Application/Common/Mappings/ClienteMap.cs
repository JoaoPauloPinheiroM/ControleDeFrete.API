using ControleDeFrete.API.Application.Common.DTOS.Responses.Clientes;
using ControleDeFrete.API.Domain.Entites;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ControleDeFrete.API.Application.Common.Mappings;

public static class ClienteMap
{
    public static DetalhesClienteResponse ToResponse (this Cliente cliente)
    {
        return new DetalhesClienteResponse
        (
            cliente.Nome,
            cliente.Documento,
            $"{cliente.Endereco.Logradouro}, {cliente.Endereco.Cidade}-{cliente.Endereco.Estado}",
            cliente.Ativo
        );
    }
}
