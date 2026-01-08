using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Fretes;

public class FinalizarEntregaDoFrete : IFinalizarEntregaDoFrete
{
    private readonly IFreteRepository _freteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public FinalizarEntregaDoFrete ( IFreteRepository freteRepository , IUnitOfWork unitOfWork )
    {
        _freteRepository = freteRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> ExecuteAsync ( string codigoFrete , int sequencia , DateOnly dataEntrega )
    {
        var dataEntregaResult = DataFato.Create( dataEntrega );
        if (dataEntregaResult.IsFailure)
            return Result.Failure( dataEntregaResult.Error! );


        var frete = await _freteRepository.GetByCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado" );


        var resultado = frete.FinalizarEntrega( dataEntrega , sequencia );
        if (resultado.IsFailure)
            return Result.Failure( resultado.Error! );


        var sucesso = await _unitOfWork.CommitAsync();
        if (!sucesso)
            return Result.Failure( "Não foi possível finalizar a entrega do frete" );
        return Result.Success();

    }


}
