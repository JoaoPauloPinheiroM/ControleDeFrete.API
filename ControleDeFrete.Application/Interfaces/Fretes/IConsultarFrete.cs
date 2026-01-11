
using ControleDeFrete.API.Application.Common.DTOS.Responses.Fretes;
using ControleDeFrete.Domain.Enums;


namespace ControleDeFretes.Application.Interfaces.Fretes;

public interface IConsultarFrete
{
    Task<DetalhesFreteResponse?> GetByIdAsync ( int idFrete );
    Task<DetalhesFreteResponse?> GetByCodigoAsync ( string codigoFrete );
    Task<IEnumerable<DetalhesFreteResponse>> GetAllAsync ( );
    Task<IEnumerable<DetalhesFreteResponse>> GetByClienteIdAsync ( string docCliente );
    Task<IEnumerable<DetalhesFreteResponse>> GetByMotoristaIdAsync ( string docMotorista );
    Task<IEnumerable<DetalhesFreteResponse>> GetByStatusAsync ( Status status );
    //Task<IEnumerable<Frete>> GetByDateTransitoRangeAsync(DateOnly startDate, DateOnly endDate);
    //Task<IEnumerable<Frete>> GetByDateFinalizadoRangeAsync(DateOnly startDate, DateOnly endDate);


}
