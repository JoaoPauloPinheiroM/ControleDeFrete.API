using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Motoristas;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Motoristas;

public class MudarStatusDoMotorista : IMudarStatusDoMotorista
{
    private readonly IMotoristaRepository _motoristaRepository;

    private readonly IUnitOfWork _unitOfWork;
    public MudarStatusDoMotorista ( IMotoristaRepository motoristaRepository , IUnitOfWork unitOfWork )
    {
        _motoristaRepository = motoristaRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<Result> Execute ( string docMotorista )
    {
        var documento = CpfCnpj.Create( docMotorista );
        if (documento.IsFailure)
            return Result.Failure( documento.Error! );

        var motoristaResult = await _motoristaRepository.ObterPorcumentoAsync( documento.Value );
        if (motoristaResult is null)
            return Result.Failure( "Motorista não encontrado." );

        var posuiFrete = await _motoristaRepository.MotoristaPossuiFreteAtivoAsync( motoristaResult.Id );


        var result = motoristaResult.Inativar( posuiFrete );
        if (result.IsFailure)
            return Result.Failure( result.Error! );

        var sucesso = await _unitOfWork.CommitAsync();
        if (!sucesso)
            return Result.Failure( "Não foi possível alterar o status do motorista." );
        return Result.Success();
    }
}

