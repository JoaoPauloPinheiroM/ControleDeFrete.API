namespace ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;

public record RegistrarPagamentoRequest
(
    string Codigo,
    DateOnly DataPagamento
);