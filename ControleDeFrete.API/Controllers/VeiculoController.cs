using ControleDeFrete.API.Application.Common.DTOS.Requests.Veiculos;
using ControleDeFrete.API.Application.Common.DTOS.Responses.Veiculos;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Veiculo;
using ControleDeFrete.Application.Interfaces.Veiculos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.Controllers;

[ApiController]
[Route( "api/veiculos" )]
public sealed class VeiculoController : ControllerBase
{
    // =========================
    // CRIAÇÃO
    // =========================

    [HttpPost]
    public async Task<ActionResult<Result>> Criar (
        [FromServices] ICriarVeiculo service ,
        [FromBody] CreateVeiculoRequest request
    )
    {
        var result = await service.Execute( request );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return Created( string.Empty , null );
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalhesVeiculoResponse>>> ConsultarTodos (
        [FromServices] IConsultarVeiculo service
    )
    {
        var veiculos = await service.GetAllVeiculosAsync();
        return Ok( veiculos );
    }

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<DetalhesVeiculoResponse>> ConsultarPorId (
        [FromServices] IConsultarVeiculo service ,
        int id
    )
    {
        var veiculo = await service.GetByIdAsync( id );

        if (veiculo is null)
            return NotFound();

        return Ok( veiculo );
    }

    [HttpGet( "placa/{placa}" )]
    public async Task<ActionResult<DetalhesVeiculoResponse>> ConsultarPorPlaca (
        [FromServices] IConsultarVeiculo service ,
        string placa
    )
    {
        var veiculo = await service.GetByPlacaAsync( placa );

        if (veiculo is null)
            return NotFound();

        return Ok( veiculo );
    }

    [HttpGet( "status/{status}" )]
    public async Task<ActionResult<IEnumerable<DetalhesVeiculoResponse>>> ConsultarPorStatus (
        [FromServices] IConsultarVeiculo service ,
        bool status
    )
    {
        var veiculos = await service.GetByStatusAsync( status );
        return Ok( veiculos );
    }

    // =========================
    // EDIÇÃO
    // =========================

    [HttpPut( "{placa}" )]
    public async Task<ActionResult<Result>> Editar (
        [FromServices] IEditarVeiculo service ,
        string placa ,
        [FromQuery] string? modelo ,
        [FromQuery] string? marca ,
        [FromQuery] int? anoDeFabricacao ,
        [FromQuery] string? novoPlaca
    )
    {
        var result = await service.Execute(
            placa ,
            modelo ,
            marca ,
            anoDeFabricacao ,
            novoPlaca
        );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    // =========================
    // STATUS
    // =========================

    [HttpPut( "{placa}/mudar-status" )]
    public async Task<ActionResult<Result>> MudarStatus (
        [FromServices] IMudarStatusDoVeiculo service ,
        string placa
    )
    {
        var result = await service.Execute( placa );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }
}
