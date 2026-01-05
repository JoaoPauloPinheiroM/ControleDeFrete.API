using ControleDeFrete.API.Application.Common.DTOS.Requests.Fretes;
using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class AdicionarEntregaController ( FreteWriteAppServices freteService ) : ControllerBase
{
    [HttpPost( "{codigo}" )]
    public async Task<IActionResult> AddEntrega ( string codigoFrete , AddEntregaFreteRequest request )
    {

        var result = await freteService.AdicionarEntrega( codigoFrete , request );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }

}
