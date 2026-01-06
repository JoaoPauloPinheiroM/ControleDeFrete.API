using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Fretes;

public class AlocarMotoristsaNoFrete : IAlocarMotoristsaNoFrete
{
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IFreteRepository _freteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AlocarMotoristsaNoFrete ( IMotoristaRepository motoristaRepository , IFreteRepository freteRepository , IUnitOfWork unitOfWork )
    {
        _motoristaRepository = motoristaRepository;
        _freteRepository = freteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync ( string codigoFrete , string documentMotorista )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if ( frete is null )
            return Result.Failure( "Frete não encontrado." );
        var documento = CpfCnpj.Create( documentMotorista );
        if (documento.IsFailure )
            return Result.Failure( "Documento do motorista inválido." );

        var motorista = await _motoristaRepository.ObterPorcumentoAsync( documento.Value );
        if ( motorista is null )
            return Result.Failure( "Motorista não encontrado." );

        if ( !motorista.Ativo )
            return Result.Failure( "Motorista inativo." );
        frete.AtribuirMotorista( motorista.Id );
        var sucesso = await _unitOfWork.CommitAsync();
        if ( !sucesso )
            return Result.Failure( "Erro ao alocar motorista no frete." );
        return Result.Success();
    }
}
