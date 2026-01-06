using ControleDeFrete.API.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Interfaces.Frete;

public interface IValidateOperatedRegion
{
    Task<Result> ValidateRegion ( string estado, string cidade );
}
