using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers;

[ApiController]
[Route( "api/[controller]" )]
public class FreteController ( FreteWriteAppServices freteService ) : ControllerBase
{
    [HttpPost "/criar"]
    public async Task<IActionResult> Create ( CreateFreteRequest request )
    {
        var result = await freteService.CreateFrete( request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }

    [HttpPost( "{codigo}/Adicionar-entregas" )]
    public async Task<IActionResult> AddEntrega ( string codigoFrete , AddEntregaFreteRequest request )
    {
        
        var result = await freteService.AdicionarEntrega( codigoFrete ,  request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }

    [HttpPut( "{codigo}/iniciar-transito" )]
    public async Task<IActionResult> IniciarTransito ( string codigo , [FromQuery] DateOnly dataCarregamento , IniciarTransitoFreteRequest request )
    {
        var result = await freteService.IniciarTransito( dataCarregamento , request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }

    [HttpPut( "{codigo}/finalizar-entrega" )]
    public async Task<IActionResult> FinalizarEntrega ( string codigo , FinalizarEntregaRequest request )
    {
        var result = await freteService.FinalizarEntrega( request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }
    [HttpPut("{codigo}/cancelar-frete")]
    public async Task<IActionResult> CancelarFrete ( string codigo , CancelarFreteRequest request )
    {
        var result = await freteService.CancelarFrete( codigo , request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }



    [HttpPatch( "{codigo}/alterar-Frete" )]
    public async Task<IActionResult> Alterar ( string codigo , AlterarFreteRequest request )
    {
        var result = await freteService.AlterarFrete( codigo , request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }

    [HttpDelete( "{codigo}" )]
    public async Task<IActionResult> Deletar ( string codigo )
    {
        var result = await freteService.DeletarFrete( codigo );
        return result.IsSuccess ? NoContent() : BadRequest( result );
    }
}
