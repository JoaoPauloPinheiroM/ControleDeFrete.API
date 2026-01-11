using ControleDeFrete.Domain.Common;

namespace ControleDeFrete.Domain.ValueObjects;

public readonly struct DataFato
{
    private readonly DateOnly _data;

    private DataFato ( DateOnly data ) => _data = data;

    //30-12-2025: joão.mourao - Cirei esse metodo para forçar o uso do Create na inicialização do Value Object
    public DateOnly Valor
    {
        get
        {
            if (_data == default)
                throw new InvalidOperationException( "DataFato não foi inicializada corretamente através do método Create." ); 
            return _data;
        }
    }

    //30-12-2025: joão.mourao - validações da criação da data do fato  especificamente na data de emissao do frete ou outros documentos com data de emissao
    public static Result<DataFato> Create ( DateOnly data )
    {
        var hoje = DateOnly.FromDateTime( DateTime.Now );

        if (data > hoje)
            return Result<DataFato>.Failure( "A data não pode ser futura." ); 
            
        if (data == default)
            return Result<DataFato>.Failure( "Data inválida." );

        return Result<DataFato>.Success( new DataFato( data ) ); 

    }

    public static implicit operator DateOnly ( DataFato d ) => d.Valor;
}
