using ControleDeFrete.API.Application.Common.DTOS.Requests.Veiculos;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.API.Domain.Entites;
using ControleDeFrete.API.Domain.Interfaces;
using ControleDeFrete.API.Domain.ValueObjects;

namespace ControleDeFrete.API.Application.Services.Write;

public class VeiculoWriteAppServices
{
    private readonly IVeiculoRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public VeiculoWriteAppServices ( IVeiculoRepository repository , IUnitOfWork unitOfWork )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Create ( CreateVeiculoRequest command )
    {
        var placaResult = Placa.Create( command.Placa );
        if (placaResult.IsFailure)
        {
            return Result.Failure( placaResult.Error! );
        }

        var VeiculoExistente = await _repository.ObterPorPlacaAsync( placaResult.Value );
        if (VeiculoExistente is not null)
        {
            return Result.Failure( "Veículo com a placa informada já está cadastrado." );
        }
        var veiculoResult = Veiculo.Create(
            placaResult.Value ,
            command.Modelo ,
            command.Marca ,
            command.AnoFabricacao
        );
        if (veiculoResult.IsFailure)
        {
            return Result.Failure( veiculoResult.Error! );
        }
        await _repository.AdicionarAsync( veiculoResult.Value! );
        var resultado = await _unitOfWork.CommitAsync();
        return resultado ? Result.Success() : Result.Failure( "Ocorreu um erro ao salvar o veiculo." );

    }
}
