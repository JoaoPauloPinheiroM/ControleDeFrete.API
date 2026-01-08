using ControleDeFrete.API.Application.Common.DTOS.Requests.Motoristas;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Clientes;
using ControleDeFrete.Application.Interfaces.Motoristas;
using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Motoristas;

public class CriarMotorista : ICriarMotorista
{
    private readonly IMotoristaRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public CriarMotorista ( IMotoristaRepository repository , IUnitOfWork unitOfWork )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async  Task<Result> Execute ( CreateMotoristaRequest motorista )
    {
        var documentoResult = CpfCnpj.Create( motorista.Documento );
        if (documentoResult.IsFailure)
            return Result.Failure( documentoResult.Error! );

        var motoristaExistente = await _repository.ObterPorcumentoAsync( documentoResult.Value );
        if (motoristaExistente is not null)
            return Result.Failure( "Já existe um motorista cadastrado com este documento." );

        var enderecoResult = Localizacao.Create( motorista.Logradouro , motorista.Cidade , motorista.Estado , null , null );
        if (enderecoResult.IsFailure)
            return Result.Failure( enderecoResult.Error! );

        var dataFato = DataFato.Create( DateOnly.FromDateTime( DateTime.Now ) );
        if (dataFato.IsFailure)
            return Result.Failure( dataFato.Error! );


        var motoristaResult = Motorista.Create( motorista.Nome , documentoResult.Value , motorista.Cnh , enderecoResult.Value! , dataFato.Value );


        if (motoristaResult.IsFailure)
            return Result.Failure( motoristaResult.Error! );

        await _repository.AdicionarAsync( motoristaResult.Value! );

        var resultado = await _unitOfWork.CommitAsync();

        return resultado ? Result.Success() : Result.Failure( "Ocorreu um erro ao salvar o motorista." );
    }
}
