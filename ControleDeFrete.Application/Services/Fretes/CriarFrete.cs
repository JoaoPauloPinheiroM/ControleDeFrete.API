using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Fretes;

public  class CriarFrete : ICriarFrete
{
    private readonly IFreteRepository _freteRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IValidatorFrete _validator;
    private readonly IUnitOfWork _unitOfWork;
    public CriarFrete ( IFreteRepository freteRepository , IUnitOfWork unitOfWork , IClienteRepository clienteRepository , IValidatorFrete validator )
    {
        _freteRepository = freteRepository;
        _unitOfWork = unitOfWork;
        _clienteRepository = clienteRepository;
        _validator = validator;
    }

    public async  Task<Result> ExecuteAsync ( CreateFreteRequest frete )
    {
        var freteExistente = await _validator.ExistsFreteAsync( frete.Codigo );
        if(freteExistente)
            return Result.Failure( "Já existe um frete com esse código." );

        var documentoClienteResult = CpfCnpj.Create( frete.DocumentoCliente );
        if (documentoClienteResult.IsFailure)
            return Result.Failure( documentoClienteResult.Error! );

        var enderecoResult = Localizacao.Create( frete.Logadouro , frete.Cidade , frete.Estado , frete.Latitude , frete.Longitude );
        if (enderecoResult.IsFailure)
            return Result.Failure( enderecoResult.Error! );

        var cliente = await _clienteRepository.ObterPorDocumentoAsync( documentoClienteResult.Value );
        if (cliente is null || !cliente.Ativo)
            return Result.Failure( "Cliente não encontrado ou não está ativo." );


        var valorFreteResult = Money.Create( frete.ValorFrete );
        if (valorFreteResult.IsFailure)
            return Result.Failure( valorFreteResult.Error! );

        var valorDescarregoResult = Money.Create( frete.ValorDescarga );
        if (valorDescarregoResult.IsFailure)
            return Result.Failure( valorDescarregoResult.Error! );

        var valorMotoristaResult = Money.Create( frete.ValorPagoMotorista );
        if (valorMotoristaResult.IsFailure)
            return Result.Failure( valorMotoristaResult.Error! );

        var dataEmissaoResult = DataFato.Create( DateOnly.FromDateTime( DateTime.Now ) );
        if (dataEmissaoResult.IsFailure)
            return Result.Failure( dataEmissaoResult.Error! );

        var freteResponse = Frete.Create( frete.Codigo , valorFreteResult.Value! , valorDescarregoResult.Value! , valorMotoristaResult.Value! , dataEmissaoResult.Value! , null , null , null , cliente.Id , null , null , enderecoResult.Value! );


        await _freteRepository.AdicionarAsync( freteResponse.Value! );
        var resultado = await _unitOfWork.CommitAsync();
        return resultado ? Result.Success() : Result.Failure( "Falha ao salvar o frete." );
    }
}
