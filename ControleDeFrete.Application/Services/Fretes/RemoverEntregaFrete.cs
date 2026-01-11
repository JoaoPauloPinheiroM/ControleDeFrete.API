using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;

namespace ControleDeFrete.Application.Services.Fretes;

public class RemoverEntregaFrete : IRemoverEntregaFrete
{

    private readonly IFreteRepository _freteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RemoverEntregaFrete ( IFreteRepository freteRepository , IUnitOfWork unitOfWork )
    {
        _freteRepository = freteRepository;
        _unitOfWork = unitOfWork;
    }




    public async Task<Result> Execute ( string codigoFrete , int sequencia )
    {
        var frete = await _freteRepository.GetByCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );

        var removerEntregaResult = frete.RemoverEntrega( sequencia );
        if (removerEntregaResult.IsFailure)
            return Result.Failure( removerEntregaResult.Error! );

        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível remover a entrega do frete." );

        return Result.Success();
    }


}
