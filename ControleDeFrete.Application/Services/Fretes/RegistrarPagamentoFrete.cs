using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;

namespace ControleDeFrete.Application.Services.Fretes;

public class RegistrarPagamentoFrete : IRegistrarPagamentoFrete
{
    private readonly IFreteRepository _freteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RegistrarPagamentoFrete ( IFreteRepository freteRepository, IUnitOfWork unitOfWork )
    {
        _freteRepository = freteRepository;
        _unitOfWork = unitOfWork;
    }
    public async  Task<Result> ExecuteAsync ( string  codigo , DateOnly dataPagamento )
    {
        var frete = await _freteRepository.GetByCodigoAsync ( codigo );
        if (frete is null)
            return Result.Failure ( "Frete não encontrado." );
        var resultado = frete.RegistrarPagamento ( dataPagamento );
        if (resultado.IsFailure)
            return Result.Failure ( resultado.Error! );
        
        var sucesso = await _unitOfWork.CommitAsync ();
        if (!sucesso)
            return Result.Failure ( "Não foi possível registrar o pagamento do frete." );
        return Result.Success ();
    }
}
