namespace ControleDeFrete.API.Application.Common.DTOS.Responses.Fretes;

public record DetalhesFreteResponse
(
    string Codigo,
    DateOnly DataEmissao,
    DateOnly? DataCarregamento,
    DateOnly? DataEntrega,
    DateOnly? DataPagamento,
    decimal Valor,
    decimal Descarga,
    decimal PagoMotorista ,
    decimal ValorTotal ,
    string Status ,
    string MotoristaNome ,
    string VeiculoPlaca ,
    string Origem ,
    string Destinos
);