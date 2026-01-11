using ControleDeFrete.API.Application.Common.DTOS.Requests.Motoristas;
using ControleDeFrete.API.Application.Common.DTOS.Requests.Veiculos;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Veiculos;
using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Veiculos;

public class CriarVeiculo : ICriarVeiculo
{
    private readonly IVeiculoRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public CriarVeiculo(IVeiculoRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async  Task<Result> Execute ( CreateVeiculoRequest veiculo )
    {
        var placaResult = Placa.Create( veiculo.Placa );
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
            veiculo.Modelo ,
            veiculo.Marca ,
            veiculo.AnoFabricacao
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
