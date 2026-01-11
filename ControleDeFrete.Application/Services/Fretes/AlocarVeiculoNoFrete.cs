using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Fretes;

public class AlocarVeiculoNoFrete : IAlocarVeiculoNoFrete
{
    private readonly IFreteRepository _freteRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AlocarVeiculoNoFrete ( IFreteRepository freteRepository , IVeiculoRepository veiculoRepository , IUnitOfWork unitOfWork )
    {
        _freteRepository = freteRepository;
        _veiculoRepository = veiculoRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> ExecuteAsync ( string codigoFrete , string placa )
    {
        var frete = await _freteRepository.GetByCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );

        var placaVeiculo = Placa.Create( placa );
        if (placaVeiculo.IsFailure)
            return Result.Failure( "Placa do veículo inválida." );
        var veiculo = await _veiculoRepository.ObterPorPlacaAsync( placaVeiculo.Value );
        if (veiculo is null)
            return Result.Failure( "Veículo não encontrado." );

        if (!veiculo.Ativo)
            return Result.Failure( "Veículo inativo." );

        var resultado = frete.AtribuirVeiculo( veiculo.Id );
        if (resultado.IsFailure)
            return Result.Failure( resultado.Error! );

        var sucesso = await _unitOfWork.CommitAsync();
        if (!sucesso)
            return Result.Failure( "Ocorreu um erro ao alocar o veículo no frete." );
        return Result.Success();
    }
}
