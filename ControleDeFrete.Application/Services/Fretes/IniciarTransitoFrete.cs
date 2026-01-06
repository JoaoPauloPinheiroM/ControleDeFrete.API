using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Fretes;

public class IniciarTransitoFrete : IIniciarTransitoFrete
{
    private readonly IFreteRepository _freteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public IniciarTransitoFrete ( IFreteRepository freteRepository , IUnitOfWork unitOfWork )
    {
        _freteRepository = freteRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> ExecuteAsync (string codigo, DateOnly dataInicioTransito )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync(codigo);
        if (frete is null)
            return Result.Failure("Frete não encontrado");
        var resultado = frete.IniciarTransito( dataInicioTransito );
        if (resultado.IsFailure)
            return Result.Failure(resultado.Error!);
        var sucesso = await _unitOfWork.CommitAsync();
        if (!sucesso)
            return Result.Failure("Não foi possível iniciar o trânsito do frete");
        return Result.Success();
    }
}
