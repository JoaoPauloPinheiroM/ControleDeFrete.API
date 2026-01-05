using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class IniciarTransitoController ( FreteWriteAppServices freteService ) : ControllerBase
{
    [HttpPut( "{codigo}" )]
    public async Task<IActionResult> IniciarTransito ( string codigo , [FromQuery] DateOnly dataCarregamento , IniciarTransitoFreteRequest request )
    {
        var result = await freteService.IniciarTransito( dataCarregamento , request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }

}
