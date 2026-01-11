using ControleDeFrete.API.Application.Common.DTOS.Responses.Veiculos;
using ControleDeFrete.Domain.Entites;

namespace ControleDeFrete.API.Application.Common.Mappings;

public static class VeiculoMap
{
    public static DetalhesVeiculoResponse ToResponse ( this Veiculo veiculo )
    {
        return new DetalhesVeiculoResponse
        (
            veiculo.Placa ,
            veiculo.Modelo ,
            veiculo.Marca ,
            veiculo.AnoFabricacao ,
            veiculo.Ativo
        );
    }
}
