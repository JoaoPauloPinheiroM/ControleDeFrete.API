
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;

namespace ControleDeFrete.Application.Services.Fretes;



public class ReabrirFrete : IReabrirFrete
{
    private readonly IFreteRepository _freteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ReabrirFrete ( IFreteRepository freteRepository , IUnitOfWork unitOfWork )
    {
        _freteRepository = freteRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result> Execute ( string codigoFrete )
    {
        var frete = await _freteRepository.GetByCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );

        var result = frete.RetornaPendente();
        if (result.IsFailure)
            return Result.Failure( result.Error! );

        var sucess = await _unitOfWork.CommitAsync();

        if (!sucess)
            return Result.Failure( "Não foi possível reabrir o frete." );

        return Result.Success();
    }
}
