using ControleDeFrete.API.Application.Common.DTOS.Requests.Clientes;
using ControleDeFrete.API.Application.Common.DTOS.Responses.Clientes;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.Controllers;

[ApiController]
[Route( "api/clientes" )]
public sealed class ClienteController : ControllerBase
{
    // =========================
    // CRIAÇÃO
    // =========================

    [HttpPost]
    public async Task<ActionResult<Result>> Criar (
        [FromServices] ICriarCliente service ,
        [FromBody] CreateClienteRequest request
    )
    {
        var result = await service.CreateClienteAsync( request );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return Created( string.Empty , null );
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<DetalhesClienteResponse>> ConsultarPorId (
        [FromServices] IConsultarCliente service ,
        int id
    )
    {
        var cliente = await service.GetByIdAsync( id );

        if (cliente is null)
            return NotFound();

        return Ok( cliente );
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalhesClienteResponse>>> ConsultarTodos (
        [FromServices] IConsultarCliente service
    )
    {
        var clientes = await service.GetAllClienteAsync();
        return Ok( clientes );
    }

    [HttpGet( "documento/{documento}" )]
    public async Task<ActionResult<DetalhesClienteResponse>> ConsultarPorDocumento (
        [FromServices] IConsultarCliente service ,
        string documento
    )
    {
        var cliente = await service.GetByDocumentAsync( documento );

        if (cliente is null)
            return NotFound();

        return Ok( cliente );
    }

    [HttpGet( "status/{status}" )]
    public async Task<ActionResult<IEnumerable<DetalhesClienteResponse>>> ConsultarPorStatus (
        [FromServices] IConsultarCliente service ,
        bool status
    )
    {
        var clientes = await service.GetByStatusAsync( status );
        return Ok( clientes );
    }

    // =========================
    // EDIÇÃO
    // =========================

    [HttpPut( "{documento}" )]
    public async Task<ActionResult<Result>> Editar (
        [FromServices] IEditarCliente service ,
        string documento ,
        [FromQuery] string? nome ,
        [FromQuery] string? novoDocumento ,
        [FromQuery] string? logradouro ,
        [FromQuery] string? cidade ,
        [FromQuery] string? estado
    )
    {
        var result = await service.Execute(
            documento ,
            nome ,
            novoDocumento ,
            logradouro ,
            cidade ,
            estado
        );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }

    // =========================
    // STATUS
    // =========================

    [HttpPut( "{documento}/mudar-status" )]
    public async Task<ActionResult<Result>> MudarStatus (
        [FromServices] IMudarStatusDoCliente service ,
        string documento
    )
    {
        var result = await service.ChangeStatusAsync( documento );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }
}
