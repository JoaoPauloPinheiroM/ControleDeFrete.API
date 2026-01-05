using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class CriarFreteController ( FreteWriteAppServices freteService ) : ControllerBase
{

    [HttpPost ("/criar")]
    public async Task<IActionResult> Create ( CreateFreteRequest request )
    {
        var result = await freteService.CreateFrete( request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }

}
