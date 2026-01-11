namespace ControleDeFrete.API.Application.Common.DTOS.Responses.Clientes;

public record DetalhesClienteResponse
(
    string Nome,
    string Documento ,
    string Endereco ,
    bool Ativo
);
