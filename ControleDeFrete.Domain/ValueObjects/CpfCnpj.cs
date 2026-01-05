using ControleDeFrete.Domain.Common;

namespace ControleDeFrete.Domain.ValueObjects;

public readonly struct CpfCnpj
{
    private readonly string _value;
    private readonly bool _isInitialized;
    public string Numero
    {
        get
        {
            if (!_isInitialized){
                throw new InvalidOperationException( "CpfCnpj não foi inicializado com o Create." );
            }
            return _value;

        }
    }
    public bool IsCpf => _value.Length == 11;
    public bool IsCnpj => _value.Length == 14;

    private CpfCnpj ( string numero )
    {
        _value = numero;
        _isInitialized = true;
    }

    public static Result<CpfCnpj> Create ( string numero )
    {
        if (string.IsNullOrWhiteSpace( numero ))
            return Result<CpfCnpj>.Failure( "O documento não pode ser vazio." );

        var limpo = new string( numero.Where( char.IsDigit ).ToArray() );

        if (limpo.Length != 11 && limpo.Length != 14)
            return Result<CpfCnpj>.Failure( "Documento deve ter 11 (CPF) ou 14 (CNPJ) dígitos." );


        return Result<CpfCnpj>.Success( new CpfCnpj( limpo ) );
    }

    public static implicit operator string ( CpfCnpj d ) => d._value;
}
