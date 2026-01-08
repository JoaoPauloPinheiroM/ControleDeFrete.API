using ControleDeFrete.API.Application.Common.DTOS.Responses.Veiculos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Veiculos;

public interface IConsultarVeiculo
{
    Task<IEnumerable<DetalhesVeiculoResponse>> GetAllVeiculosAsync ( );
    Task<DetalhesVeiculoResponse?> GetByPlacaAsync ( string placa );
    Task<DetalhesVeiculoResponse?> GetByIdAsync ( int veiculoId );
    Task<IEnumerable<DetalhesVeiculoResponse>> GetByStatusAsync ( bool status );

}
