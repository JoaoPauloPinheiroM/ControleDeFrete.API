using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class AlterarFreteController ( FreteWriteAppServices freteService ) : ControllerBase
{
    [HttpPatch( "{codigo}" )]
    public async Task<IActionResult> AlterarFrete ( string codigo , AlterarFreteRequest request )
    {
        var result = await freteService.AlterarFrete( codigo , request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }

}
