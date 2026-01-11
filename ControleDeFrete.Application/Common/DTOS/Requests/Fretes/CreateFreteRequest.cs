namespace ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;

public record CreateFreteRequest
(
    string Codigo,
    string DocumentoCliente ,
    decimal ValorFrete,
    decimal ValorDescarga,
    decimal ValorPagoMotorista,
    string Logadouro, string Cidade, string Estado,
    double Latitude ,
    double Longitude
);
