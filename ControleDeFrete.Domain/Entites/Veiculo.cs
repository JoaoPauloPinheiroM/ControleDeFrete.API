using ControleDeFrete.Domain.Common;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Domain.Entites;

public sealed class Veiculo
{
    public int Id { get; private set; }
    public Placa Placa { get; private set; }
    public string Modelo { get; private set; } = null!;
    public string Marca { get; private set; } = null!;
    public bool Ativo { get; private set; }
    public int AnoFabricacao { get; private set; }

    private Veiculo () { }
    private Veiculo ( Placa placa , string modelo , string marca , int anoFabricacao )
    {
        Placa = placa;
        Modelo = modelo.Trim();
        Marca = marca.Trim();
        AnoFabricacao = anoFabricacao;
        Ativo = true;
    }
    public Result AtualizarPlaca ( string novaPlacaRaw )
    {
        var result = Placa.Create( novaPlacaRaw );
        if (result.IsFailure) return Result.Failure( result.Error! );

        this.Placa = result.Value;
        return Result.Success();
    }

    public Result AtualizarModelo ( string novoModelo )
    {
        if (string.IsNullOrWhiteSpace( novoModelo ))
            return Result.Failure( "O modelo não pode ser vazio." );
        this.Modelo = novoModelo.Trim();
        return Result.Success();
    }
    public Result AtualizarMarca ( string novaMarca )
    {
        if (string.IsNullOrWhiteSpace( novaMarca ))
            return Result.Failure( "A marca não pode ser vazia." );
        this.Marca = novaMarca.Trim();
        return Result.Success();
    }
    public Result AtualizarAnoFabricacao ( int novoAno )
    {
        var anoAtual = DateTime.Now.Year;
        if (novoAno < 1900 || novoAno > anoAtual)
            return Result.Failure( $"O ano de fabricação deve estar entre 1900 e {anoAtual}." );
        this.AnoFabricacao = novoAno;
        return Result.Success();
    }
    public Result  Inativar(bool possuiFreteEmCurso )
    {
        if (this.Ativo && possuiFreteEmCurso)
        {
            return Result.Failure( "O veículo não pode ser inativado enquanto possuir frete em curso." );
        }
        Ativo = false;
        return Result.Success();
    }
    public Result Ativar()
    {
        if (this.Ativo)
        {
            return Result.Success();
        }
        Ativo = true;
        return Result.Success();
    }

    public static Result<Veiculo> Create ( Placa placa , string modelo , string marca , int anoFabricacao  )
    {
        var placaResult = Placa.Create( placa );
        if (placaResult.IsFailure)
            return Result<Veiculo>.Failure( placaResult.Error! );
        if (string.IsNullOrWhiteSpace( modelo ))
            return Result<Veiculo>.Failure( "O modelo do veículo não pode ser vazio." );
        if (string.IsNullOrWhiteSpace( marca ))
            return Result<Veiculo>.Failure( "A marca do veículo não pode ser vazia." );
        var anoAtual = DateTime.Now.Year;
        if (anoFabricacao < 1900 || anoFabricacao > anoAtual)
            return Result<Veiculo>.Failure( $"O ano de fabricação deve estar entre 1900 e {anoAtual}." );
        return Result<Veiculo>.Success( new Veiculo ( placaResult.Value , modelo , marca , anoFabricacao ) );
    }


}
