namespace ControleDeFrete.API.Application.Common.DTOS.Requests.Clientes;

public record CreateClienteRequest
(
    string Nome ,
    string Documento ,
    string Logradouro ,
    string Cidade ,
    string Estado

);
