using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class DeletarFreteController ( FreteWriteAppServices freteService ) : ControllerBase
{
    [HttpDelete( "{codigo}" )]
    public async Task<IActionResult> Deletar ( string codigo )
    {
        var result = await freteService.DeletarFrete( codigo );
        return result.IsSuccess ? NoContent() : BadRequest( result );
    }


}
