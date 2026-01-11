using ControleDeFrete.API.Application.Common.DTOS.Responses.Motoristas;
using ControleDeFrete.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Motoristas;

public interface IConsultarMotorista
{
    Task<DetalhesMotoristaResponse?> GetByIdAsync(int motoristaId);
    Task<IEnumerable<DetalhesMotoristaResponse>> GetAllMotoristaAsync();
    Task<DetalhesMotoristaResponse?> GetByDocumentAsync(string documento);
    Task<IEnumerable<DetalhesMotoristaResponse>> GetByStatusAsync (bool status);
}
