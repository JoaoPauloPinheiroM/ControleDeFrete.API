using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class RemoverEntregaController ( FreteWriteAppServices freteService ) : ControllerBase
{
    [HttpPost( "{codigo}" )]
    public async Task<IActionResult> RemoverEntrega ( string codigo , int seq )
    {
        var result = await freteService.RemoverEntrega( codigo , seq );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }
}
