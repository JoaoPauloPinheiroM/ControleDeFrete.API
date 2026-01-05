using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class AlterarEntregaController ( FreteWriteAppServices freteService ) : ControllerBase
{
    [HttpPatch( "{codigo}" )]
    public async Task<IActionResult> AlterarEntrega ( string codigo , int seq , AlterarEntregaRequest request )
    {
        var result = await freteService.AlterarEntrega( codigo , seq , request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }
}
