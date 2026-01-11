using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Veiculos;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Veiculos;

public class MudarStatusDoVeiculo : IMudarStatusDoVeiculo
{
    private readonly IVeiculoRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public MudarStatusDoVeiculo ( IVeiculoRepository repository , IUnitOfWork unitOfWork )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Execute ( string codPlaca )
    {
        var placa = Placa.Create( codPlaca );
        if (placa.IsFailure)
            return Result.Failure( placa.Error! );

        var veiculoResult = await _repository.ObterPorPlacaAsync( placa.Value );
        if (veiculoResult is null)
            return Result.Failure( "Veiculo não encontrado." );

        var posuiFrete = await _repository.VeiculoPossuiFreteAtivoAsync( veiculoResult.Id );


        if(veiculoResult.Ativo)
        {
            var result = veiculoResult.Inativar( posuiFrete );

            if (result.IsFailure)
                return Result.Failure( result.Error! );
        }
        else
        {
            var result = veiculoResult.Ativar();
            if (result.IsFailure)
                return Result.Failure( result.Error! );
        }


        var sucesso = await _unitOfWork.CommitAsync();
        if (!sucesso)
            return Result.Failure( "Não foi possível alterar o status do veiculo." );
        return Result.Success();
    }
}
