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
    public DateOnly DataEntrega { get; private set; }

    internal void MarcarEntregue ( DateOnly dataEntrega )
    {
        this.Entregue = true;
        this.DataEntrega = dataEntrega;
    }

    private Entrega ( ) { }

    private Entrega ( int freteId, int clienteId, int Sequencia, string? obs, Localizacao destino )
    {
        FreteId = freteId;
        ClienteId = clienteId;
        this.Sequencia = Sequencia;
        Observacoes = obs;
        Destino = destino;
        Entregue = false;
    }

    public static Result<Entrega> Create ( int freteId, int clienteId, int Sequencia, string? obs, Localizacao destino )
    {
        if (freteId <= 0)
            return Result<Entrega>.Failure("FreteId inválido.");
        if (clienteId <= 0)
            return Result<Entrega>.Failure("ClienteId inválido.");
        if (Sequencia <= 0)
            return Result<Entrega>.Failure("Sequência inválida.");

        return Result<Entrega>.Success(new Entrega (freteId, clienteId,  Sequencia, obs, destino ));


    }
}
