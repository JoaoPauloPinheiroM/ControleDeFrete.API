
using ControleDeFrete.API.Application.Common.DTOS.Responses.Fretes;
using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Enums;


namespace ControleDeFretes.Application.Interfaces.Fretes;

public interface IConsultarFrete
{
    Task<DetalhesFreteResponse?> GetByIdAsync(int idFrete);
    Task<IEnumerable<DetalhesFreteResponse>> GetAllAsync();
    Task<IEnumerable<DetalhesFreteResponse>> GetByClienteIdAsync(int idCliente);
    Task<IEnumerable<DetalhesFreteResponse>> GetByMotoristaIdAsync(int idMotorista);
    Task<IEnumerable<DetalhesFreteResponse>> GetByStatusAsync(Status status);
    //Task<IEnumerable<Frete>> GetByDateTransitoRangeAsync(DateOnly startDate, DateOnly endDate);
    //Task<IEnumerable<Frete>> GetByDateFinalizadoRangeAsync(DateOnly startDate, DateOnly endDate);


}
