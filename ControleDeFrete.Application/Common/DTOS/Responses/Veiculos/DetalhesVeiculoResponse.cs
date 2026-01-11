namespace ControleDeFrete.API.Application.Common.DTOS.Responses.Veiculos;

public record DetalhesVeiculoResponse
(
    string Placa ,
    string Modelo ,
    string Marca ,
    int AnoFabricacao ,
    bool Ativo
);

