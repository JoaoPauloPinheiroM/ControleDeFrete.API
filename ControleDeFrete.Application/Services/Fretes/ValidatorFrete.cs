using ControleDeFrete.Application.Interfaces.Fretes;
using ControleDeFrete.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeFrete.Application.Services.Fretes;

public class ValidatorFrete : IValidatorFrete
{
    private readonly IFreteRepository _freteRepository;
    public ValidatorFrete ( IFreteRepository freteRepository )
    {
        _freteRepository = freteRepository;
    }
    public async Task<bool> ExistsFreteAsync ( string codigo )
    {
        var freteExistente = await _freteRepository.ObterFretePorCodigoAsync( codigo );
        if (freteExistente is not null)
            return true;
        return false;
    }
}
