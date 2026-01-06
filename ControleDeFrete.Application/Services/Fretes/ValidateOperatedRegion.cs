using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.Application.Interfaces.Frete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Fretes;

public class ValidateOperatedRegion : IValidateOperatedRegion
{
    // Validar se a região (estado e cidade) é operada pela empresa de frete
    //Irei usar em um futuro próximo para validar as regiões operadas
    public Task<Result> ValidateRegion ( string estado , string cidade )
    {
        throw new NotImplementedException();
    }
}
