using ControleDeFrete.Domain.Common;
using ControleDeFrete.Domain.Enums;
using ControleDeFrete.Domain.ValueObjects;

namespace ControleDeFrete.Domain.Entites;

public class Cliente
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public CpfCnpj Documento { get; private set; }
    public TipoPessoa TipoPessoa => Documento.IsCpf ? TipoPessoa.Fisica : TipoPessoa.Juridica;
    public Localizacao Endereco { get; private set; }
    public bool Ativo { get; private set; }
    private Cliente ( ) { }
    private Cliente ( string nome , CpfCnpj documento , Localizacao endereco )
    {
        Nome = nome;
        Documento = documento;
        Endereco = endereco;
        Ativo = true;
    }
    public static Result<Cliente> Create ( string nome , CpfCnpj documento , Localizacao endereco )
    {
        if(nome is null ||  (nome.Length > 250 || nome.Length < 3))
            return Result<Cliente>.Failure( "Nome do cliente inválido. Deve conter entre 3 e 250 caracteres." );
        if (string.IsNullOrWhiteSpace( nome ) )
            return Result<Cliente>.Failure( "Nome do cliente não pode ser vazio." );
        return Result<Cliente>.Success( new Cliente( nome , documento , endereco ) );
    }
    public Result Inativar ( bool possuiFreteEmCurso )
    {
        if (this.Ativo && possuiFreteEmCurso)
        {
            return Result.Failure( "O Cliente não pode ser inativado enquanto possuir frete em curso." );
        }
        Ativo = false;

        return Result.Success();
    }
    public Result Ativar ( )
    {
        if (this.Ativo)
        {
            return Result.Success();
        }
        Ativo = true;
        return Result.Success();
    }
    public Result AlterarEndereco ( bool possuiFreteEmCurso , Localizacao endereco )
    {
        if (this.Ativo && possuiFreteEmCurso)
        {
            return Result.Failure( "O Cliente não pode ter o endereço atualizado enquanto possuir frete em curso." );
        }
        Endereco = endereco;
        return Result.Success();
    }
    public Result AlterarNome ( string novoNome )
    {
        if (string.IsNullOrWhiteSpace( novoNome ))
            return Result.Failure( "O nome do cliente não pode ser vazio." );
        this.Nome = novoNome.Trim();
        return Result.Success();
    }


}
