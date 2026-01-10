using ControleDeFrete.Domain.Common;

namespace ControleDeFrete.Domain.ValueObjects;


public readonly struct CpfCnpj
{
    public string Numero { get; }

    public bool IsCpf => Numero.Length == 11;
    public bool IsCnpj => Numero.Length == 14;

    private CpfCnpj ( string numero )
    {
        Numero = numero;
    }

    public static Result<CpfCnpj> Create ( string input )
    {
        if (string.IsNullOrWhiteSpace( input ))
            return Result<CpfCnpj>.Failure( "Documento não pode ser vazio." );

        var numero = new string( input.Where( char.IsDigit ).ToArray() );

        if (numero.Length != 11 && numero.Length != 14)
            return Result<CpfCnpj>.Failure( "Documento deve ter 11 (CPF) ou 14 (CNPJ) dígitos." );

        return Result<CpfCnpj>.Success( new CpfCnpj( numero ) );
    }

    public override string ToString ( ) => Numero;
}
