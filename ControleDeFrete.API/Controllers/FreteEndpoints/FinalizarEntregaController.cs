using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class FinalizarEntregaController ( FreteWriteAppServices freteService ) : ControllerBase
{
    [HttpPut( "{codigo}" )]
    public async Task<IActionResult> FinalizarEntrega ( string codigo , FinalizarEntregaRequest request )
    {
        var result = await freteService.FinalizarEntrega( request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }
}
