namespace ControleDeFrete.API.Application.Common.DTOS.Requests.Motoristas;

public record CreateMotoristaRequest
(
    string Nome ,
    string Documento , 
    string Cnh ,
    string Logradouro ,
    string Cidade ,
    string Estado

 );
