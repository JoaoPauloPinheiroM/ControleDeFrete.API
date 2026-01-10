using ControleDeFrete.API.Application.Common.DTOS.Requests.Motoristas;
using ControleDeFrete.API.Application.Common.DTOS.Responses.Motoristas;
using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Motoristas;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class MotoristaController : ControllerBase
{
    // =========================
    // CRIAR
    // =========================
    [HttpPost]
    public async Task<ActionResult<Result>> Criar (
        [FromServices] ICriarMotorista service ,
        string nome ,
        string documento ,
        string cnh ,
        string logradouro ,
        string cidade ,
        string estado
    )
    {
        var request = new CreateMotoristaRequest(
            nome ,
            documento ,
            cnh ,
            logradouro ,
            cidade ,
            estado
        );

        var result = await service.Execute( request );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return Created( string.Empty , null );
    }

    // =========================
    // CONSULTAR
    // =========================
    [HttpGet( "{documento}" )]
    public async Task<ActionResult<DetalhesMotoristaResponse>> ConsultarPorDocumento (
        [FromServices] IConsultarMotorista service ,
        string documento
    )
    {
        var result = await service.GetByDocumentAsync( documento );

        if (result is null)
            return NotFound();

        return Ok( result );
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalhesMotoristaResponse>>> ConsultarTodos (
        [FromServices] IConsultarMotorista service
    )
    {
        var result = await service.GetAllMotoristaAsync();
        return Ok( result );
    }

    [HttpGet( "status/{ativo}" )]
    public async Task<ActionResult<IEnumerable<DetalhesMotoristaResponse>>> ConsultarPorStatus (
        [FromServices] IConsultarMotorista service ,
        bool ativo
    )
    {
        var result = await service.GetByStatusAsync( ativo );
        return Ok( result );
    }

    // =========================
    // EDITAR
    // =========================
    [HttpPut( "{documento}" )]
    public async Task<ActionResult<Result>> Editar (
        [FromServices] IEditarMotorista service ,
        string documento ,
        string? nome ,
        string? cnh ,
        string? novoDocumento ,
        string? logradouro ,
        string? cidade ,
        string? estado
    )
    {
        var result = await service.Execute(
            documento ,
            nome ,
            cnh ,
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
    // MUDAR STATUS
    // =========================
    [HttpPut( "{documento}/mudar-status" )]
    public async Task<ActionResult<Result>> MudarStatus (
        [FromServices] IMudarStatusDoMotorista service ,
        string documento
    )
    {
        var result = await service.Execute( documento );

        if (!result.IsSuccess)
            return BadRequest( result.Error );

        return NoContent();
    }
}
