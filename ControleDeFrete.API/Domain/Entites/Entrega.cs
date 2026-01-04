using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.API.Domain.ValueObjects;

namespace ControleDeFrete.API.Domain.Entites;

public class Entrega
{
    public int Id { get; private set; }
    public int FreteId { get; private set; }
    public int ClienteId { get; private set; }
    public int Sequencia { get; private set; }
    public bool Entregue { get; private set; }
    public string Observacoes { get; private set; } = string.Empty;
    public Localizacao Destino { get; private set; }
    public DateOnly? DataEntrega { get; private set; }

    internal void MarcarEntregue ( DateOnly dataEntrega )
    {
        this.Entregue = true;
        this.DataEntrega = dataEntrega;
    }

    private Entrega ( ) { }

    private Entrega ( int freteId , int clienteId , int Sequencia , string? obs , Localizacao destino )
    {
        FreteId = freteId;
        ClienteId = clienteId;
        this.Sequencia = Sequencia;
        Observacoes = obs ?? string.Empty;
        Destino = destino;
        Entregue = false;
    }

    public static Result<Entrega> Create ( int freteId , int clienteId , int Sequencia , string? obs , Localizacao destino )
    {
        if (freteId <= 0)
            return Result<Entrega>.Failure( "FreteId inválido." );
        if (clienteId <= 0)
            return Result<Entrega>.Failure( "ClienteId inválido." );
        if (Sequencia <= 0)
            return Result<Entrega>.Failure( "Sequência inválida." );

        return Result<Entrega>.Success( new Entrega( freteId , clienteId , Sequencia , obs , destino ) );


    }

    public  Result AlterarObservacoes ( string? obs )
    {
        if (obs != null && obs.Length > 500)
            return Result.Failure( "Observações não podem exceder 500 caracteres." );
        if (this.Entregue)
            return Result.Failure( "Não é possível atualizar observações de uma entrega já realizada." );
        this.Observacoes = obs ?? string.Empty;
        return Result.Success();

    }
    public Result AlterarDestino ( Localizacao novoDestino )
    {
        if (Entregue)
            return Result.Failure( "Não é possível atualizar o destino de uma entrega já realizada." );
        this.Destino = novoDestino;
        return Result.Success();
    }
    public Result AlterarCliente ( int novoClienteId )
    {
        if (novoClienteId <= 0)
            return Result.Failure( "ClienteId inválido." );
        if (Entregue)
            return Result.Failure( "Não é possível atualizar o cliente de uma entrega já realizada." );
        this.ClienteId = novoClienteId;
        return Result.Success();
    }


}
