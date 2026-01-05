using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class ReabrirFreteController ( FreteWriteAppServices freteService ) : ControllerBase
{
    [HttpPut( "{codigo}" )]
    public async Task<IActionResult> ReabrirFrete ( string codigo )
    {
        var result = await freteService.ReabrirFrete( codigo );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }
}
