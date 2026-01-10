using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Motoristas;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Motoristas;

public class EditarMotorista : IEditarMotorista
{
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditarMotorista (
        IMotoristaRepository motoristaRepository ,
        IUnitOfWork unitOfWork
    )
    {
        _motoristaRepository = motoristaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Execute (
        string documento ,
        string? nome ,
        string? cnh ,
        string? novoDocumento ,
        string? logradouro ,
        string? cidade ,
        string? estado
    )
    {
        // Documento atual
        var docResult = CpfCnpj.Create( documento );
        if (docResult.IsFailure)
            return Result.Failure( docResult.Error! );

        var motorista = await _motoristaRepository.ObterPorcumentoAsync( docResult.Value );
        if (motorista is null)
            return Result.Failure( "Motorista não encontrado." );

        var possuiFreteAtivo = await _motoristaRepository.MotoristaPossuiFreteAtivoAsync( motorista.Id );

        // Alterar documento
        if (!string.IsNullOrWhiteSpace( novoDocumento ))
        {
            var novoDocResult = CpfCnpj.Create( novoDocumento );
            if (novoDocResult.IsFailure)
                return Result.Failure( novoDocResult.Error! );

            var res = motorista.AtualizarDocumento( possuiFreteAtivo , novoDocResult.Value );
            if (res.IsFailure)
                return Result.Failure( res.Error! );
        }

        // Alterar nome
        if (!string.IsNullOrWhiteSpace( nome ))
        {
            var res = motorista.AtualizarNome( possuiFreteAtivo , nome );
            if (res.IsFailure)
                return Result.Failure( res.Error! );
        }

        // Alterar CNH
        if (!string.IsNullOrWhiteSpace( cnh ))
        {
            var res = motorista.AtualizarCnh( possuiFreteAtivo , cnh );
            if (res.IsFailure)
                return Result.Failure( res.Error! );
        }

        // Alterar endereço (parcial permitido)
        if (!string.IsNullOrWhiteSpace( logradouro ) ||
            !string.IsNullOrWhiteSpace( cidade ) ||
            !string.IsNullOrWhiteSpace( estado ))
        {
            var enderecoResult = Localizacao.Create(
                logradouro! ,
                cidade! ,
                estado! ,
                null ,
                null
            );

            if (enderecoResult.IsFailure)
                return Result.Failure( enderecoResult.Error! );

            var res = motorista.AtualizarEndereco( possuiFreteAtivo , enderecoResult.Value! );
            if (res.IsFailure)
                return Result.Failure( res.Error! );
        }

        var sucesso = await _unitOfWork.CommitAsync();
        if (!sucesso)
            return Result.Failure( "Não foi possível salvar as alterações do motorista." );

        return Result.Success();
    }
}
