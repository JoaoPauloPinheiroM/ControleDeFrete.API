using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Application.Services.Fretes;

public class AdicionarEntregaFrete : IAdicionarEntregaFrete
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFreteRepository _freteRepository;
    private readonly IClienteRepository _clienteRepository;

    public AdicionarEntregaFrete ( IUnitOfWork unitOfWork , IFreteRepository freteRepository , IClienteRepository clienteRepository )
    {
        _unitOfWork = unitOfWork;
        _freteRepository = freteRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<Result> Execute ( string codigoFrete , CreateEntregaFreteRequest entrega )
    {
        var frete = await _freteRepository.GetByCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );


        var documentoCliente = CpfCnpj.Create( entrega.DocumentoCliente );
        if (documentoCliente.IsFailure)
            return Result.Failure( documentoCliente.Error! );


        var cliente = await _clienteRepository.GetByDocument( documentoCliente.Value );
        if (cliente is null)
            return Result.Failure( "Cliente não encontrado." );

        if(!cliente.Ativo)
            return Result.Failure( "Cliente inativo." );

        var destino = Localizacao.Create(
            entrega.Logradouro ,
            entrega.Cidade ,
            entrega.Estado ,
            entrega.Latitude ,
            entrega.Longitude );
        if (destino.IsFailure)
            return Result.Failure( destino.Error! );


        var entregaResult = frete.AdicionarEntrega(
            cliente.Id ,
            entrega.Observacoes ,
            destino.Value! );


        if (entregaResult.IsFailure)
            return Result.Failure( entregaResult.Error! );


        var sucesso = await _unitOfWork.CommitAsync();


        if (!sucesso)
            return Result.Failure( "Não foi possível adicionar a entrega ao frete." );


        return Result.Success();
    }
}
