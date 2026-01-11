using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Common.DTOS.Responses.Fretes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Enums;
using ControleDeFretes.Application.Interfaces.Fretes;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.Controllers;

[ApiController]
[Route( "api/fretes" )]
public sealed class FreteController : ControllerBase
{
    // =========================
    // CRIAÇÃO
    // =========================

    [HttpPost]
    public async Task<ActionResult<Result>> CriarFrete (
        [FromServices] ICriarFrete service ,
        [FromQuery] CreateFreteRequest request
    )
    {
        var result = await service.ExecuteAsync( request );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return Created( string.Empty , null );
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<DetalhesFreteResponse>> ConsultarPorId (
        [FromServices] IConsultarFrete service ,
        int id
    )
    {
        var frete = await service.GetByIdAsync( id );

        if (frete is null)
            return NotFound();

        return Ok( frete );
    }
    [HttpGet( "{codigo}" )]
    public async Task<ActionResult<DetalhesFreteResponse>> ConsultarPorCodigo (
        [FromServices] IConsultarFrete service ,
        string codigo
    )
    {
        var frete = await service.GetByCodigoAsync( codigo );

        if (frete is null)
            return NotFound();

        return Ok( frete );
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalhesFreteResponse>>> ConsultarTodos (
        [FromServices] IConsultarFrete service
    )
    {
        var fretes = await service.GetAllAsync();
        return Ok( fretes );
    }

    [HttpGet( "cliente/{documento}" )]
    public async Task<ActionResult<IEnumerable<DetalhesFreteResponse>>> ConsultarPorCliente (
        [FromServices] IConsultarFrete service ,
        string documento
    )
    {
        var fretes = await service.GetByClienteIdAsync( documento );
        return Ok( fretes );
    }

    [HttpGet( "motorista/{documento}" )]
    public async Task<ActionResult<IEnumerable<DetalhesFreteResponse>>> ConsultarPorMotorista (
        [FromServices] IConsultarFrete service ,
        string documento
    )
    {
        var fretes = await service.GetByMotoristaIdAsync( documento );
        return Ok( fretes );
    }

    [HttpGet( "status/{status}" )]
    public async Task<ActionResult<IEnumerable<DetalhesFreteResponse>>> ConsultarPorStatus (
        [FromServices] IConsultarFrete service ,
        Status status
    )
    {
        var fretes = await service.GetByStatusAsync( status );
        return Ok( fretes );
    }

    // =========================
    // ALOCAÇÕES
    // =========================

    [HttpPut( "{codigo}/alocar-motorista" )]
    public async Task<ActionResult<Result>> AlocarMotorista (
        [FromServices] IAlocarMotoristsaNoFrete service ,
        string codigo ,
        [FromQuery] string documentoMotorista
    )
    {
        var result = await service.ExecuteAsync( codigo , documentoMotorista );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    [HttpPut( "{codigo}/alocar-veiculo" )]
    public async Task<ActionResult<Result>> AlocarVeiculo (
        [FromServices] IAlocarVeiculoNoFrete service ,
        string codigo ,
        [FromQuery] string placa
    )
    {
        var result = await service.ExecuteAsync( codigo , placa );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    // =========================
    // ENTREGAS
    // =========================

    [HttpPost( "{codigo}/entregas" )]
    public async Task<ActionResult<Result>> AdicionarEntrega (
        [FromServices] IAdicionarEntregaFrete service ,
        string codigo ,
        [FromQuery] CreateEntregaFreteRequest request
    )
    {
        var result = await service.Execute( codigo , request );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    [HttpDelete( "{codigo}/entregas/{sequencia:int}" )]
    public async Task<ActionResult<Result>> RemoverEntrega (
        [FromServices] IRemoverEntregaFrete service ,
        string codigo ,
        int sequencia
    )
    {
        var result = await service.Execute( codigo , sequencia );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    [HttpPut( "{codigo}/entregas/{sequencia:int}/finalizar" )]
    public async Task<ActionResult<Result>> FinalizarEntrega (
        [FromServices] IFinalizarEntregaDoFrete service ,
        string codigo ,
        int sequencia ,
        [FromQuery] DateOnly dataEntrega
    )
    {
        var result = await service.ExecuteAsync( codigo , sequencia , dataEntrega );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    // =========================
    // CICLO DO FRETE
    // =========================

    [HttpPut( "{codigo}/iniciar-transito" )]
    public async Task<ActionResult<Result>> IniciarTransito (
        [FromServices] IIniciarTransitoFrete service ,
        string codigo ,
        [FromQuery] DateOnly dataInicioTransito
    )
    {
        var result = await service.ExecuteAsync( codigo , dataInicioTransito );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    [HttpPut( "{codigo}/cancelar" )]
    public async Task<ActionResult<Result>> Cancelar (
        [FromServices] ICancelarFrete service ,
        string codigo
    )
    {
        var result = await service.ExecuteAsync( codigo );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    [HttpPut( "{codigo}/reabrir" )]
    public async Task<ActionResult<Result>> Reabrir (
        [FromServices] IReabrirFrete service ,
        string codigo
    )
    {
        var result = await service.Execute( codigo );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    [HttpPut( "{codigo}/registrar-pagamento" )]
    public async Task<ActionResult<Result>> RegistrarPagamento (
        [FromServices] IRegistrarPagamentoFrete service ,
        string codigo ,
        [FromQuery] DateOnly dataPagamento
    )
    {
        var result = await service.ExecuteAsync( codigo , dataPagamento );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    // =========================
    // EXCLUSÃO
    // =========================

    [HttpDelete( "{codigo}" )]
    public async Task<ActionResult<Result>> Excluir (
        [FromServices] IExcluirFrete service ,
        string codigo
    )
    {
        var result = await service.ExecuteAsync( codigo );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }
}
