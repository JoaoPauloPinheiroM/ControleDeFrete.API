using ControleDeFrete.API.Application.Common.DTOS.Responses.Clientes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Enums;

namespace ControleDeFrete.Application.Interfaces.Clientes;

public interface IConsultarCliente
{
    Task<DetalhesClienteResponse?> GetByIdAsync ( int clienteId );
    Task<IEnumerable<DetalhesClienteResponse>> GetAllClienteAsync ( );
    Task<DetalhesClienteResponse?> GetByDocumentAsync ( string document );
    Task<IEnumerable<DetalhesClienteResponse>> GetByStatusAsync ( bool status );

}
