using ControleDeFrete.API.Application.Common.Result;

namespace ControleDeFrete.API.Domain.ValueObjects;

public readonly struct Placa
{
    private readonly string _value;
    private readonly bool _isInitialized;
    public string Valor
    {
       get {
            if (!_isInitialized)
                throw new InvalidOperationException( "Placa não foi inicializada corretamente através do método Create." );
            return _value;
        }
    }
    private Placa ( string value )
    {
        _value = value;
        _isInitialized = true;
    }
    public static Result<Placa> Create ( string placa )
    {
        if (string.IsNullOrWhiteSpace( placa ))
            return Result<Placa>.Failure( "A placa não pode ser vazia." );
        var limpo = placa.ToUpper().Trim();
        if (limpo.Length != 7)
            return Result<Placa>.Failure( "A placa deve ter exatamente 7 caracteres." );
        return Result<Placa>.Success( new Placa( limpo ) );
    }
    public static implicit operator string ( Placa p ) => p._value;
}
