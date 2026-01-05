namespace ControleDeFrete.API.Application.Common.DTOS.Requests.Veiculos;

public record CreateVeiculoRequest
(
    string Placa,
    string Modelo,
    string Marca,
    int AnoFabricacao
);

