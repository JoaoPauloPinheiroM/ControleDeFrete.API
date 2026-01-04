namespace ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;

public record AlterarEntregaRequest
(
    string? DocumentoCliente ,
    string? Logradouro ,
    string? Cidade ,
    string? Estado ,
    double? Latitude ,
    double? Longitude,
    string? Observacao
);
