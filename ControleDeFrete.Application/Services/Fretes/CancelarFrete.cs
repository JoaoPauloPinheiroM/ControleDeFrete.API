using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;

namespace ControleDeFrete.Application.Services.Fretes;

public class CancelarFrete : ICancelarFrete
{
    private readonly IFreteRepository _freteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CancelarFrete ( IFreteRepository freteRepository , IUnitOfWork unitOfWork )
    {
        _freteRepository = freteRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> ExecuteAsync ( string codigoFrete )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if (frete is null)
            return  Result.Failure( "Frete n��o encontrado."  );
        var resultado = frete.Cancelar();
        if (resultado.IsFailure)
            return  Result.Failure( resultado.Error ?? "Erro ao cancelar o frete." ) ;
        var sucesso = await _unitOfWork.CommitAsync();
        if (!sucesso)
            return  Result.Failure( "Erro ao salvar tentar Cancelar o frete." );
        return  Result.Success();

    }
}
