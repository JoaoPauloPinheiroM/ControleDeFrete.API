using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class CancelarFreteController ( FreteWriteAppServices freteService ) : ControllerBase
{
    [HttpPut( "{codigo}" )]
    public async Task<IActionResult> CancelarFrete ( string codigo )
    {
        var result = await freteService.CancelarFrete( codigo );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }
}
