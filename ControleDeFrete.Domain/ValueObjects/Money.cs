using ControleDeFrete.Domain.Common;

namespace ControleDeFrete.Domain.ValueObjects;

public readonly struct Money
{
    private readonly decimal _value;


    private Money ( decimal value )
    {
        _value = value;
    }

    public decimal Valor => _value;

    public static Result<Money> Create ( decimal value )
    {
        if (value < 0) return Result<Money>.Failure( "Valor não pode ser negativo." ); 
        return Result<Money>.Success( new Money( value ) ); 
    }

    public static Money operator +(Money a, Money b ) => new Money(a._value + b._value);
    public static Money operator -(Money a, Money b ) => new Money(a._value - b._value);
    public static Money From ( decimal value ) => new Money( value );
    public static bool operator >(Money a, Money b ) => a._value > b._value;
    public static bool operator <(Money a, Money b ) => a._value < b._value;


    
}