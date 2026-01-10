using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Clientes;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;
using System.Runtime.InteropServices;

namespace ControleDeFrete.Application.Services.Clientes;

public class MudarStatusDoCliente : IMudarStatusDoCliente
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public MudarStatusDoCliente ( IClienteRepository clienteRepository , IUnitOfWork unitOfWork )
    {
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ChangeStatusAsync ( string documentoCliente )
    {
        var documentoResult = CpfCnpj.Create( documentoCliente );
        if (documentoResult.IsFailure)
            return Result.Failure( documentoResult.Error! );

        var cliente = await _clienteRepository.GetByDocument( documentoResult.Value );
        if (cliente is null)
            return Result.Failure( "Cliente não encontrado." );

        var possuiFretesAtivos = await _clienteRepository.GetFreteAtivo( cliente.Id );


        if (cliente.Ativo)
        {
            var resultado = cliente.Inativar( possuiFretesAtivos );
            if (resultado.IsFailure)
                return Result.Failure( resultado.Error! );
        }
        else
        {
            var resultado = cliente.Ativar();  
            if (resultado.IsFailure)
                return Result.Failure( resultado.Error! );
        }

            var sucesso = await _unitOfWork.CommitAsync();
        if (!sucesso)
            return Result.Failure( "Ocorreu um erro ao tentar mudar o status do cliente." );
        return Result.Success();



    }
}
