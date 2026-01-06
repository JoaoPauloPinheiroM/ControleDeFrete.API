using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.API.Application.Services.Write;

public class FreteWriteAppServices
{
    private readonly IFreteRepository _freteRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMotoristaRepository _motoristaRepository;

    public FreteWriteAppServices (
        IFreteRepository freteRepository ,
        IClienteRepository clienteRepository ,
        IVeiculoRepository veiculoRepository ,
        IMotoristaRepository motoristaRepository ,
        IUnitOfWork unitOfWork )
    {
        _freteRepository = freteRepository;
        _clienteRepository = clienteRepository;
        _veiculoRepository = veiculoRepository;
        _motoristaRepository = motoristaRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> AdicionarEntrega ( string codigoFrete , CreateEntregaFreteRequest frete )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if (frete is null)
        {
            return Result.Failure( "Frete não encontrado." );
        }

        var documento = CpfCnpj.Create( frete.DocumentoCliente );
        if (documento.IsFailure)
        {
            return Result.Failure( documento.Error! );
        }
        var clienteId = await _clienteRepository.ObterPorDocumentoAsync( documento.Value );
        if (clienteId is null)
        {
            return Result.Failure( "Cliente não encontrado." );
        }

        var destinoResult = Localizacao.Create(
            frete.Logradouro ,
            frete.Cidade ,
            frete.Estado ,
            frete.Latitude ,
            frete.Longitude );
        if (destinoResult.IsFailure)
        {
            return Result.Failure( destinoResult.Error! );
        }

        var entregaResult = frete.AdicionarEntrega( clienteId.Id , frete.Observacoes , destinoResult.Value );
        if (entregaResult.IsFailure)
        {
            return Result.Failure( entregaResult.Error! );
        }

        var resutado = await _unitOfWork.CommitAsync();
        return resutado
            ? Result.Success()
            : Result.Failure( "Ocorreu um erro ao adicionar a entrega ao frete." );

    }


    public async Task<Result> ReabrirFrete ( string codigoFrete )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );
        var reaberturaResult = frete.RetornaPendente();
        if (reaberturaResult.IsFailure)
            return Result.Failure( reaberturaResult.Error! );
        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível reabrir o frete." );
        return Result.Success();
    }
    public async Task<Result> RemoverEntrega ( string codigoFrete , int sequencialEntrega )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );
        var removerEntregaResult = frete.RemoverEntrega( sequencialEntrega );
        if (removerEntregaResult.IsFailure)
            return Result.Failure( removerEntregaResult.Error! );
        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível remover a entrega do frete." );
        return Result.Success();
    }
}
