using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Clientes;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;
using System.Reflection.Metadata.Ecma335;

namespace ControleDeFrete.Application.Services.Clientes;

public class EditarCliente : IEditarCliente
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClienteRepository _clienteRepository;
    public EditarCliente ( IUnitOfWork unitOfWork , IClienteRepository clienteRepository )
    {
        _unitOfWork = unitOfWork;
        _clienteRepository = clienteRepository;
    }

    public async Task<Result> Execute (
        string documento ,
        string? nome ,
        string? documentoNovo ,
        string? logradouro ,
        string? cidade ,
        string? estado
    )
    {
        var docResult = CpfCnpj.Create( documento );
        if (docResult.IsFailure)
            return Result.Failure( docResult.Error! );

        var cliente = await _clienteRepository.GetByDocument( docResult.Value );
        if (cliente is null)
            return Result.Failure( "Cliente não encontrado." );

        var possuiFreteAtivo = await _clienteRepository.GetFreteAtivo( cliente.Id );

        if (!string.IsNullOrWhiteSpace( documentoNovo ))
        {
            var novoDocResult = CpfCnpj.Create( documentoNovo );
            if (novoDocResult.IsFailure)
                return Result.Failure( novoDocResult.Error! );

            var res = cliente.AlterarDocumento( possuiFreteAtivo , novoDocResult.Value );
            if (res.IsFailure)
                return Result.Failure( res.Error! );
        }

        if (!string.IsNullOrWhiteSpace( nome ))
        {
            var res = cliente.AlterarNome( possuiFreteAtivo , nome );
            if (res.IsFailure)
                return Result.Failure( res.Error! );
        }

        if (!string.IsNullOrWhiteSpace( logradouro ) ||
            !string.IsNullOrWhiteSpace( cidade ) ||
            !string.IsNullOrWhiteSpace( estado ))
        {
            var enderecoResult = Localizacao.Create( logradouro! , cidade! , estado!, null, null );
            if (enderecoResult.IsFailure)
                return Result.Failure( enderecoResult.Error! );

            cliente.AlterarEndereco( possuiFreteAtivo, enderecoResult.Value! );
        }

        var sucesso = await _unitOfWork.CommitAsync();
        if (!sucesso)
            return Result.Failure( "Não foi possível salvar as alterações." );

        return Result.Success();
    }
}
