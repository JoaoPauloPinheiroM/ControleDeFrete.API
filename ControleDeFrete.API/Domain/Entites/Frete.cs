using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.API.Domain.Enums;
using ControleDeFrete.API.Domain.ValueObjects;

namespace ControleDeFrete.API.Domain.Entites;

public sealed class Frete
{
    public int Id { get; private set; }
    public string Codigo { get; private set; } = null!;
    public Status Status { get; private set; }

    public Money Valor { get; private set; }
    public Money ValorDescarrego { get; private set; }
    public Money ValorMotorista { get; private set; }
    public Money ValorTotal => Valor + ValorDescarrego;

    public DateOnly? DataEntrega { get; private set; }
    public DataFato DataEmissao { get; private set; }
    public DateOnly? DataCarregamento { get; private set; }
    public DateOnly? DataPagamento { get; private set; }

    public int? MotoristaId { get; private set; }
    public int? VeiculoId { get; private set; }
    public int ClienteId { get; private set; }

    public Localizacao Origem { get; private set; }
    private readonly List<Entrega> _entregas = [];
    public IReadOnlyCollection<Entrega> Entregas => _entregas.AsReadOnly();

    private Frete ( ) { }

    public bool IsPendente ( ) => this.Status == Status.Pendente;
    public bool EmTransito ( ) => this.Status == Status.EmTransito;
    public bool IsFinalizado ( ) => this.Status == Status.Finalizado;
    public bool IsPago ( ) => this.Status == Status.Pago;
    public bool IsCancelado ( ) => this.Status == Status.Cancelado;
    public Result IniciarTransito ( DateOnly dataInicio)
    {
        if (!IsPendente())
        {
            return Result.Failure( "Somente fretes pendentes podem ser iniciados." );
        }
        if (MotoristaId is null || VeiculoId is null)
        {
            return Result.Failure( "Não é possível iniciar o transito de um frete sem motorista e veiculo" );
        }
        this.Status = Status.EmTransito;
        this.DataCarregamento = dataInicio;
        return Result.Success();
    }
    public Result FinalizarEntrega ( DateOnly dataEntrega , int seq )
    {
        if (!EmTransito())
        {
            return Result.Failure( "Somente fretes em trânsito podem ser finalizados." );
        }

        var resData = ValidarCronologia( this.DataEmissao.Valor , dataEntrega , this.DataCarregamento , null );
        if (resData.IsFailure) return resData;

        var entrega = _entregas.FirstOrDefault( e => e.Sequencia == seq );
        if (entrega is null)
        {
            return Result.Failure( $"Entrega com sequência {seq} não encontrada para o frete." );
        }

        entrega.MarcarEntregue( dataEntrega );

        if (_entregas.All( e => e.Entregue ))
        {
            this.Status = Status.Finalizado;
            this.DataEntrega = dataEntrega;
        }
        return Result.Success();
    }
    public Result RegistrarPagamento ( DateOnly dataPagamento )
    {
        if (!IsFinalizado())
        {
            return Result.Failure( "Somente fretes finalizados podem ser pagos." );
        }
        var resData = ValidarCronologia( this.DataEmissao.Valor , this.DataEntrega , this.DataCarregamento , dataPagamento );
        if (resData.IsFailure) return resData;
        this.Status = Status.Pago;
        this.DataPagamento = dataPagamento;
        return Result.Success();
    }
    public Result Cancelar ( )
    {
        if (IsPago() || IsFinalizado() || EmTransito())
        {
            return Result.Failure( $"Fretes com status {this.Status.ToString()} não podem ser cancelados." );
        }
        this.Status = Status.Cancelado;
        return Result.Success();
    }
    public Result RetornaPendente ( )
    {
        if (IsPago() || IsFinalizado())
        {
            return Result.Failure( $"Fretes com status {this.Status.ToString()} não podem retornar para pendente." );
        }
        this.DataCarregamento = null;
        this.Status = Status.Pendente;
        return Result.Success();
    }
    public Result AdicionarEntrega ( int clienteId, string? obs, Localizacao destino )
    {
        if (!IsPendente())
        {
            return Result.Failure( "Somente fretes pendentes podem ter entregas adicionadas." );
        }
        var proxSequencia = _entregas.Count + 1;


        var entregaResult = Entrega.Create( this.Id , clienteId , proxSequencia , obs , destino );
        if (entregaResult.IsFailure) return Result.Failure( entregaResult.Error!);

        _entregas.Add( entregaResult.Value! );
        return Result.Success();
    }
    private static Result ValidarCronologia ( DateOnly emissao , DateOnly? entrega , DateOnly? carga , DateOnly? pgto )
    {
        if (carga.HasValue && carga < emissao) return Result.Failure( "Carregamento não pode ser anterior à emissão." );
        if (entrega.HasValue && carga.HasValue && entrega < carga) return Result.Failure( "Entrega não pode ser anterior ao carregamento." );
        if (pgto.HasValue && (!entrega.HasValue || pgto < entrega)) return Result.Failure( "Pagamento exige entrega concluída e data coerente." );

        return Result.Success();
    }
    private static  Result ValidarValores ( Money valorFrete , Money valorDesc , Money valorMot )
    {
        if (valorMot > valorFrete)
        {
            return Result.Failure( "Valor do motorista não pode ser maior do que o valor do frete." );
        }
        if (valorFrete < Money.From(0))
        {
            return Result.Failure( "Valor do frete não pode ser negativo." );
        }
        if (valorDesc < Money.From( 0 ))
        {
            return Result.Failure( "Valor do descarrego não pode ser negativo." );
        }
        if ( valorMot < Money.From( 0 ))
        {
            return Result.Failure( "Valor pago ao motorista não pode ser negativo." );
        }

        return Result.Success();
    }
    public static Result<Frete> Create ( string codigo , Money valorFrete , Money valorDesc , Money valorMot , DataFato dataEmissao , DateOnly? dataEntrega , DateOnly? dataCarregamento , DateOnly? dataPagamaento , int clienteId , int motoristaId , int veiculoId , Localizacao origem )
    {

        if (string.IsNullOrWhiteSpace( codigo )) return "Código do frete é obrigatório.";

        var resValores = ValidarValores( valorFrete , valorDesc , valorMot );
        if (resValores.IsFailure) return resValores.Error!;


        var resDatas = ValidarCronologia( dataEmissao.Valor , dataEntrega , dataCarregamento , dataPagamaento );
        if (resDatas.IsFailure) return resDatas.Error!;

        if (clienteId <= 0 || motoristaId <= 0 || veiculoId <= 0) return "Entidades relacionadas inválidas.";


        return Result<Frete>.Success( new Frete( codigo , valorFrete , valorDesc , valorMot , dataEmissao , dataEntrega , dataCarregamento , dataPagamaento , clienteId , motoristaId , veiculoId , origem ) );
    }
    private Frete ( string codigo , Money valor , Money valorDescarrego , Money valorMotorista , DataFato dataEmissao , DateOnly? dataEntrega , DateOnly? dataCarregamento , DateOnly? dataPagamaento ,int remetenteId , int motoristaId , int veiculoId , Localizacao origem )
    {
        Codigo = codigo;
        Status = Status.Pendente;
        Valor = valor;
        ValorDescarrego = valorDescarrego;
        ValorMotorista = valorMotorista;
        DataEmissao = dataEmissao;
        DataEntrega = dataEntrega;
        DataCarregamento = dataCarregamento;
        DataPagamento = dataPagamaento;
        ClienteId = remetenteId;
        MotoristaId = motoristaId;
        VeiculoId = veiculoId;
        Origem = origem;
    }
}
