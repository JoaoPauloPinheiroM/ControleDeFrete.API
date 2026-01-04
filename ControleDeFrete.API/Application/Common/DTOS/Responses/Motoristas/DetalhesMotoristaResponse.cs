namespace ControleDeFrete.API.Application.Common.DTOS.Responses.Motoristas;

public record DetalhesMotoristaResponse
(
    string Nome,
    string Documento ,
    string Cnh ,
    string Endereco ,
    bool Ativo 
);