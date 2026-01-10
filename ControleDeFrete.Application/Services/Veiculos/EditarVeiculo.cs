using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Veiculo;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Veiculos;

public sealed class EditarVeiculo : IEditarVeiculo
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditarVeiculo (
        IVeiculoRepository veiculoRepository ,
        IUnitOfWork unitOfWork
    )
    {
        _veiculoRepository = veiculoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Execute (
        string placa ,
        string? modelo ,
        string? marca ,
        int? anoDeFabricacao ,
        string? novoPlaca
    )
    {
        // Placa atual
        var placaResult = Placa.Create( placa );
        if (placaResult.IsFailure)
            return Result.Failure( placaResult.Error! );

        var veiculo = await _veiculoRepository.ObterPorPlacaAsync( placaResult.Value );
        if (veiculo is null)
            return Result.Failure( "Veículo não encontrado." );

        // Nova placa
        if (!string.IsNullOrWhiteSpace( novoPlaca ))
        {
            var res = veiculo.AtualizarPlaca( novoPlaca );
            if (res.IsFailure)
                return Result.Failure( res.Error! );
        }

        // Modelo
        if (!string.IsNullOrWhiteSpace( modelo ))
        {
            var res = veiculo.AtualizarModelo( modelo );
            if (res.IsFailure)
                return Result.Failure( res.Error! );
        }

        // Marca
        if (!string.IsNullOrWhiteSpace( marca ))
        {
            var res = veiculo.AtualizarMarca( marca );
            if (res.IsFailure)
                return Result.Failure( res.Error! );
        }

        // Ano de fabricação
        if (anoDeFabricacao.HasValue)
        {
            var res = veiculo.AtualizarAnoFabricacao( anoDeFabricacao.Value );
            if (res.IsFailure)
                return Result.Failure( res.Error! );
        }

        var sucesso = await _unitOfWork.CommitAsync();
        if (!sucesso)
            return Result.Failure( "Não foi possível salvar as alterações do veículo." );

        return Result.Success();
    }
}
