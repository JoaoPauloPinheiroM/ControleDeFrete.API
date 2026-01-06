using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Fretes;

public class ExcluirFrete : IExcluirFrete
{
    private readonly IFreteRepository _freteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ExcluirFrete ( IFreteRepository freteRepository, IUnitOfWork unitOfWork )
    {
        _freteRepository = freteRepository;
        _unitOfWork = unitOfWork;
    }
    public async  Task<Result> ExecuteAsync ( string codigoFrete )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );
        if (!frete.PodeSerRemovido())
            return Result.Failure( "O frete não pode ser removido no estado atual." );
        await _freteRepository.RemoverFreteAsync( frete );
        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível deletar o frete." );
        return Result.Success();
    }
}
