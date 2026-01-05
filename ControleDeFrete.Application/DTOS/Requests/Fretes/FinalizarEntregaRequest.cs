namespace ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;

public record FinalizarEntregaRequest
(
    string Codigo,
    DateOnly DataEntrega,
    int SequencialEntrega

);
