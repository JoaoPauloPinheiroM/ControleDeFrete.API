namespace ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;

public record IniciarTransitoFreteRequest
(
    string CodigoFrete,
    string DocumentoMotorista,
    string PlacaVeiculo
);
