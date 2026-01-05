namespace ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;

public record AddEntregaFreteRequest 
    (
        string DocumentoCliente,
        string Logradouro ,
        string Cidade ,
        string Estado ,
        double Latitude ,
        double Longitude,
        string? Observacoes
    );
    