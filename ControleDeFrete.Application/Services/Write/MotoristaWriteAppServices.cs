using ControleDeFrete.API.Application.Common.DTOS.Requests.Motoristas;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.API.Application.Services.Write;

public class MotoristaWriteAppServices
{
    private readonly IMotoristaRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public MotoristaWriteAppServices ( IUnitOfWork unitOfWork , IMotoristaRepository repository )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result> Create ( CreateMotoristaRequest command )
    {
        var documentoResult = CpfCnpj.Create( command.Documento );
        if (documentoResult.IsFailure)
            return Result.Failure( documentoResult.Error! );

        var motoristaExistente = await _repository.ObterDocumentoAsync( documentoResult.Value );
        if (motoristaExistente is not null)
            return Result.Failure( "Já existe um motorista cadastrado com este documento." );

        var enderecoResult = Localizacao.Create( command.Logradouro , command.Cidade , command.Estado , null , null );
        if (enderecoResult.IsFailure)
            return Result.Failure( enderecoResult.Error! );

        var dataFato = DataFato.Create( DateOnly.FromDateTime( DateTime.Now ) );
        if (dataFato.IsFailure)
            return Result.Failure( dataFato.Error! );
        var motoristaResult = Motorista.Create( command.Nome , documentoResult.Value , command.Cnh , enderecoResult.Value , dataFato.Value );
        if (motoristaResult.IsFailure)
            return Result.Failure( motoristaResult.Error! );

        await _repository.AdicionarAsync( motoristaResult.Value! );

        var resultado = await _unitOfWork.CommitAsync();

        return resultado ? Result.Success() : Result.Failure( "Ocorreu um erro ao salvar o motorista." );
    }
}
