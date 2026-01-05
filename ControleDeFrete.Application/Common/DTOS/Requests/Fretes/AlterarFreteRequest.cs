namespace ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;

public record AlterarFreteRequest
(
    string? Codigo ,
    string? DocumentoCliente ,
    string? DocumentoMotorista ,
    DateOnly? DataEmissao,
    string? PlacaVeiculo ,
    decimal? ValorFrete ,
    decimal? ValorDescarga ,
    decimal? ValorPagoMotorista ,
    string? Logadouro , string? Cidade , string? Estado ,
    double? Latitude ,
    double? Longitude
);
