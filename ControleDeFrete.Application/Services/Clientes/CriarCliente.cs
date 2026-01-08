using ControleDeFrete.API.Application.Common.DTOS.Requests.Clientes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Clientes;
using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Clientes;

public class CriarCliente : ICriarCliente
{
    private readonly IClienteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public CriarCliente ( IClienteRepository repository , IUnitOfWork unitOfWork )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result> CreateClienteAsync ( CreateClienteRequest cliente )
    {
        var documentoResult = CpfCnpj.Create( cliente.Documento );
        if (documentoResult.IsFailure)
            return Result.Failure( documentoResult.Error! );

        var enderecoResult = Localizacao.Create( cliente.Logradouro , cliente.Cidade , cliente.Estado , null , null );
        if (enderecoResult.IsFailure)
            return Result.Failure( enderecoResult.Error! );


        var clienteExistente = await _repository.ObterPorDocumentoAsync( documentoResult.Value );
        if (clienteExistente is not null)
            return Result.Failure( "Já existe um cliente cadastrado com este documento." );


        var clienteResult = Cliente.Create( cliente.Nome , documentoResult.Value , enderecoResult.Value! );
        if (clienteResult.IsFailure)
            return Result.Failure( clienteResult.Error! );

        await _repository.AdicionarAsync( clienteResult.Value! );

        var resultado = await _unitOfWork.CommitAsync();
        return resultado ? Result.Success() : Result.Failure( "Ocorreu um erro ao salvar o cliente." );
    }
}
