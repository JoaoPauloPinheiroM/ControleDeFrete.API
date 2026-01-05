using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Domain.Entites;
using ControleDeFrete.Domain.Interfaces;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.API.Application.Services.Write;

public class FreteWriteAppServices
{
    private readonly IFreteRepository _freteRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IUnitOfWork _unitOfWork;

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
    public async Task<Result> AdicionarEntrega ( string codigoFrete , AddEntregaFreteRequest command )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if (frete is null)
        {
            return Result.Failure( "Frete não encontrado." );
        }

        var documento = CpfCnpj.Create( command.DocumentoCliente );
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
            command.Logradouro ,
            command.Cidade ,
            command.Estado ,
            command.Latitude ,
            command.Longitude );
        if (destinoResult.IsFailure)
        {
            return Result.Failure( destinoResult.Error! );
        }

        var entregaResult = frete.AdicionarEntrega( clienteId.Id , command.Observacoes , destinoResult.Value );
        if (entregaResult.IsFailure)
        {
            return Result.Failure( entregaResult.Error! );
        }

        var resutado = await _unitOfWork.CommitAsync();
        return resutado
            ? Result.Success()
            : Result.Failure( "Ocorreu um erro ao adicionar a entrega ao frete." );

    }

    public async Task<Result> CreateFrete ( CreateFreteRequest command )
    {
        var freteExistente = await _freteRepository.ObterFretePorCodigoAsync( command.Codigo );
        if (freteExistente is not null)
            return Result.Failure( "Já existe um frete cadastrado com este código." );

        var documentoClienteResult = CpfCnpj.Create( command.DocumentoCliente );

        if (documentoClienteResult.IsFailure)
            return Result.Failure( documentoClienteResult.Error! );

        var enderecoResult = Localizacao.Create( command.Logadouro , command.Cidade , command.Estado , command.Latitude , command.Longitude );

        if (enderecoResult.IsFailure)
            return Result.Failure( enderecoResult.Error! );

        var cliente = await _clienteRepository.ObterPorDocumentoAsync( documentoClienteResult.Value );

        if (cliente is null || !cliente.Ativo)
            return Result.Failure( "Cliente não encontrado ou não está ativo." );


        var valorFreteResult = Money.Create( command.ValorFrete );

        if (valorFreteResult.IsFailure)
            return Result.Failure( valorFreteResult.Error! );

        var valorDescarregoResult = Money.Create( command.ValorDescarga );

        if (valorDescarregoResult.IsFailure)
            return Result.Failure( valorDescarregoResult.Error! );

        var valorMotoristaResult = Money.Create( command.ValorPagoMotorista );

        if (valorMotoristaResult.IsFailure)
            return Result.Failure( valorMotoristaResult.Error! );

        var dataEmissaoResult = DataFato.Create( DateOnly.FromDateTime( DateTime.Now ) );


        var moneyFrete = valorFreteResult.Value!;
        var moneyDescarga = valorDescarregoResult.Value!;
        var moneyMotorista = valorMotoristaResult.Value!;
        var dataEmissao = dataEmissaoResult.Value!;
        var origem = enderecoResult.Value!;


        var freteResponse = Frete.Create( command.Codigo , moneyFrete , moneyDescarga , moneyMotorista , dataEmissao , null , null , null , cliente.Id , null , null , origem );


        await _freteRepository.AdicionarAsync( freteResponse.Value! );
        var resultado = await _unitOfWork.CommitAsync();
        return resultado ? Result.Success() : Result.Failure( "Falha ao salvar o frete." );
    }

    public async Task<Result> IniciarTransito ( DateOnly dataCarregamento , IniciarTransitoFreteRequest command )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( command.CodigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );

        var veiculo = await _veiculoRepository.ObterPorPlacaAsync( command.PlacaVeiculo );
        if (veiculo is null)
            return Result.Failure( "Veículo não encontrado." );

        var documentoMotorista = CpfCnpj.Create( command.DocumentoMotorista );
        if (documentoMotorista.IsFailure)
            return Result.Failure( documentoMotorista.Error! );

        var motorista = await _motoristaRepository.ObterDocumentoAsync( documentoMotorista.Value );
        if (motorista is null)
            return Result.Failure( "Motorista não encontrado." );

        var dataCarregamentoResul = DataFato.Create( dataCarregamento );
        if (dataCarregamentoResul.IsFailure)
            return Result.Failure( dataCarregamentoResul.Error! );

        var freteResult = frete.IniciarTransito( dataCarregamentoResul.Value , motorista.Id , veiculo.Id  );
        if (freteResult.IsFailure)
            return Result.Failure( freteResult.Error! );

        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível iniciar o trânsito do frete." );

        return Result.Success();

    }

    public async Task<Result> FinalizarEntrega ( FinalizarEntregaRequest request )
    {
        var dataEntregaResult = DataFato.Create( request.DataEntrega );

        var frete = await _freteRepository.ObterFretePorCodigoAsync( request.Codigo );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );

        var entrega = frete.Entregas.FirstOrDefault( e => e.Sequencia == request.SequencialEntrega );
        if (entrega is null)
        {
            return Result.Failure( $"Entrega com sequência {request.SequencialEntrega} não encontrada para o frete." );
        }


        var entregaResult = frete.FinalizarEntrega( dataEntregaResult.Value , entrega );
        if (entregaResult.IsFailure)
            return Result.Failure( entregaResult.Error! );


        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível finalizar a entrega do frete." );
        return Result.Success();
    }

    public async Task<Result> CancelarFrete ( string codigoFrete )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );
        var cancelamentoResult = frete.Cancelar();
        if (cancelamentoResult.IsFailure)
            return Result.Failure( cancelamentoResult.Error! );

        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível cancelar o frete." );
        return Result.Success();
    }

    public async Task<Result> DeletarFrete ( string codigoFrete )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );
        if (!frete.PodeSerRemovido())
            return Result.Failure( "O frete não pode ser removido no estado atual." );
        await _freteRepository.RemoverFreteAsync( frete );
        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível deletar o frete." );
        return Result.Success();
    }

    public async Task<Result> AlterarFrete ( string codigo , AlterarFreteRequest request )
    {
        if (request == null)
            return Result.Failure( "Requisição inválida." );

        if (string.IsNullOrWhiteSpace( codigo ))
            return Result.Failure( "Código do frete inválido." );

        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigo );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );

        if (request.ValorFrete.HasValue || request.ValorDescarga.HasValue || request.ValorPagoMotorista.HasValue)
        {
            var novoValorFrete = request.ValorFrete.HasValue ? Money.Create( request.ValorFrete.Value ).Value : frete.Valor;
            var novoValorDescarga = request.ValorDescarga.HasValue ? Money.Create( request.ValorDescarga.Value ).Value : frete.ValorDescarrego;
            var novoValorMotorista = request.ValorPagoMotorista.HasValue ? Money.Create( request.ValorPagoMotorista.Value ).Value : frete.ValorMotorista;
            var atualizarValoresResult = frete.AtualizarValores( novoValorFrete , novoValorDescarga , novoValorMotorista );
            if (atualizarValoresResult.IsFailure)
                return Result.Failure( atualizarValoresResult.Error! );
        }

        if (!string.IsNullOrEmpty( request.Codigo ))
        {
            var atualizarCodigoRes = frete.AlterarCodigo( codigo );
            if (atualizarCodigoRes.IsFailure)
                return Result.Failure( atualizarCodigoRes.Error! );
        }

        var novaLocalizacaoResult = Localizacao.Create(
            request.Logadouro ?? frete.Origem.Logradouro ,
            request.Cidade ?? frete.Origem.Cidade ,
            request.Estado ?? frete.Origem.Estado ,
            request.Latitude ?? frete.Origem.Latitude ,
            request.Longitude ?? frete.Origem.Longitude );
        if (novaLocalizacaoResult.IsFailure)
            return Result.Failure( novaLocalizacaoResult.Error! );
        var atualizarLocalizacaoResult = frete.AlterarOrigem( novaLocalizacaoResult.Value );
        if (atualizarLocalizacaoResult.IsFailure)
            return Result.Failure( atualizarLocalizacaoResult.Error! );

        if (!string.IsNullOrEmpty( request.DocumentoCliente ))
        {
            var documentoClienteResult = CpfCnpj.Create( request.DocumentoCliente );
            if (documentoClienteResult.IsFailure)
                return Result.Failure( documentoClienteResult.Error! );
            var cliente = await _clienteRepository.ObterPorDocumentoAsync( documentoClienteResult.Value );
            if (cliente is null || !cliente.Ativo)
                return Result.Failure( "Cliente não encontrado ou não está ativo." );
            var atualizarClienteResult = frete.AlterarClienteOrigem( cliente.Id );
            if (atualizarClienteResult.IsFailure)
                return Result.Failure( atualizarClienteResult.Error! );
        }
        if (!string.IsNullOrEmpty( request.DocumentoMotorista ))
        {
            var documentoMotoristaResult = CpfCnpj.Create( request.DocumentoMotorista );
            if (documentoMotoristaResult.IsFailure)
                return Result.Failure( documentoMotoristaResult.Error! );
            var motorista = await _motoristaRepository.ObterDocumentoAsync( documentoMotoristaResult.Value );
            if (motorista is null || !motorista.Ativo)
                return Result.Failure( "Motorista não encontrado ou não está ativo." );
            var atualizarMotoristaResult = frete.AlterarMotorista( motorista.Id );
            if (atualizarMotoristaResult.IsFailure)
                return Result.Failure( atualizarMotoristaResult.Error! );
        }
        if (!string.IsNullOrEmpty( request.PlacaVeiculo ))
        {
            var veiculo = await _veiculoRepository.ObterPorPlacaAsync( request.PlacaVeiculo );
            if (veiculo is null || !veiculo.Ativo)
                return Result.Failure( "Veículo não encontrado ou não está ativo." );
            var atualizarVeiculoResult = frete.AlterarVeiculo( veiculo.Id );
            if (atualizarVeiculoResult.IsFailure)
                return Result.Failure( atualizarVeiculoResult.Error! );
        }
        if (request.DataEmissao.HasValue)
        {
            var novaDataEmissaoResult = DataFato.Create( request.DataEmissao.Value );
            if (novaDataEmissaoResult.IsFailure)
                return Result.Failure( novaDataEmissaoResult.Error! );
            var atualizarDataEmissaoResult = frete.AlterarDataEmissao( novaDataEmissaoResult.Value );
            if (atualizarDataEmissaoResult.IsFailure)
                return Result.Failure( atualizarDataEmissaoResult.Error! );
        }

        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível alterar o frete." );
        return Result.Success();

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

    public async Task<Result> AlterarEntrega ( string codigoFrete , int seq , AlterarEntregaRequest request )
    {
        if (request == null)
            return Result.Failure( "Requisição inválida." );
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );


        var entregaAtual = frete.Entregas.FirstOrDefault( e => e.Sequencia == seq );
        if (entregaAtual == null)
            return Result.Failure( $"Entrega com sequência {seq} não encontrada." );
        var novaLocalizacaoResult = Localizacao.Create(
                                    request.Logradouro ?? entregaAtual.Destino.Logradouro ,
                                    request.Cidade ?? entregaAtual.Destino.Cidade ,
                                    request.Estado ?? entregaAtual.Destino.Estado ,
                                    request.Latitude ?? entregaAtual.Destino.Latitude ,
                                    request.Longitude ?? entregaAtual.Destino.Longitude );
        if (novaLocalizacaoResult.IsFailure)
            return Result.Failure( novaLocalizacaoResult.Error! );



        var atualizarLocalizacaoResult = frete.AlterarDestinoEntrega( novaLocalizacaoResult.Value , seq );
        if (atualizarLocalizacaoResult.IsFailure)
            return Result.Failure( atualizarLocalizacaoResult.Error! );

        if (!string.IsNullOrEmpty( request.DocumentoCliente ))
        {
            var documentoClienteResult = CpfCnpj.Create( request.DocumentoCliente );
            if (documentoClienteResult.IsFailure)
                return Result.Failure( documentoClienteResult.Error! );
            var cliente = await _clienteRepository.ObterPorDocumentoAsync( documentoClienteResult.Value );
            if (cliente is null || !cliente.Ativo)
                return Result.Failure( "Cliente não encontrado ou não está ativo." );
            var atualizarClienteResult = frete.AlterarClienteEntrega( cliente.Id , seq );
            if (atualizarClienteResult.IsFailure)
                return Result.Failure( atualizarClienteResult.Error! );
        }

        if (!string.IsNullOrEmpty( request.Observacao ))
        {
            var atualizarObservacoesResult = frete.AlterarObservacoesEntrega( request.Observacao , seq );
            if (atualizarObservacoesResult.IsFailure)
                return Result.Failure( atualizarObservacoesResult.Error! );
        }

        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível alterar a entrega." );
        return Result.Success();
    }

    public async Task<Result> RegistrarPagamento ( string codigoFrete , DateOnly dataPagamento )
    {
        var frete = await _freteRepository.ObterFretePorCodigoAsync( codigoFrete );
        if (frete is null)
            return Result.Failure( "Frete não encontrado." );
        var dataFato = DataFato.Create( dataPagamento );
        var pagamentoResult = frete.RegistrarPagamento( dataFato.Value );
        if (pagamentoResult.IsFailure)
            return Result.Failure( pagamentoResult.Error! );
        var resultado = await _unitOfWork.CommitAsync();
        if (!resultado)
            return Result.Failure( "Não foi possível registrar o pagamento do frete." );
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
