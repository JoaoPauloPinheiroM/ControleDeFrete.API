using ControleDeFrete.API.Application.Common.DTOS.Requests.Clientes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.API.Domain.Entites;
using ControleDeFrete.API.Domain.Interfaces;
using ControleDeFrete.API.Domain.ValueObjects;

namespace ControleDeFrete.API.Application.Services.Write;

public class ClienteWriteAppServices
{
    private readonly IClienteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ClienteWriteAppServices ( IClienteRepository repository , IUnitOfWork unitOfWork )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Create ( CreateClienteRequest command )
    {
        var documentoResult = CpfCnpj.Create( command.Documento );
        if (documentoResult.IsFailure)
            return Result.Failure( documentoResult.Error! );

        var enderecoResult = Localizacao.Create( command.Logradouro , command.Cidade , command.Estado , null , null );
        if (enderecoResult.IsFailure)
            return Result.Failure( enderecoResult.Error! );


        var clienteExistente = await _repository.ObterPorDocumentoAsync( documentoResult.Value );
        if (clienteExistente is not null)
            return Result.Failure( "Já existe um cliente cadastrado com este documento." );


        var clienteResult = Cliente.Create( command.Nome , documentoResult.Value , enderecoResult.Value );
        if (clienteResult.IsFailure)
            return Result.Failure( clienteResult.Error! );

        await _repository.AdicionarAsync( clienteResult.Value! );

        var resultado = await _unitOfWork.CommitAsync();
        return resultado ? Result.Success() : Result.Failure( "Ocorreu um erro ao salvar o cliente." );
    }
}
