using ControleDeFrete.API.Application.Services.Write;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFrete.API.APIControllers.FreteEndpoints;

[ApiController]
[Route( "api/[controller]" )]
public class RegistrarPagamentoController ( FreteWriteAppServices freteService ) : ControllerBase
{

    [HttpPut( "{codigo}" )]
    public async Task<IActionResult> RegistrarPagamento ( string codigo , DateOnly dataPagamento )
    {
        var result = await freteService.RegistrarPagamento( codigo , dataPagamento );
        return result.IsSuccess ? Ok( result ) : BadRequest( result );
    }


}
