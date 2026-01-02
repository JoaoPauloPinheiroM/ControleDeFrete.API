using ControleDeFrete.API.Application.Common.Result;
using ControleDeFrete.API.Domain.Enums;
using ControleDeFrete.API.Domain.ValueObjects;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ControleDeFrete.API.Domain.Entites;

public sealed class Motorista
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public CpfCnpj Documento { get; private set; }
    public TipoPessoa TipoPessoa => Documento.IsCpf ? TipoPessoa.Fisica : TipoPessoa.Juridica;
    public string Cnh { get; private set; } = null!;
    public Localizacao Endereco { get; private set; }
    public DataFato DataCadastro   { get; private set; }
    public bool Ativo { get; private set; }
    private Motorista() { }

    private  Motorista(string nome, CpfCnpj cpf, string cnh, Localizacao endereco, DataFato dataCadastro)
    {
        Nome = nome;
        Documento = cpf;
        Cnh = cnh;
        Endereco = endereco;
        DataCadastro = dataCadastro;
        Ativo = true;
        
    }
    public static Result<Motorista> Create ( string nome , CpfCnpj cpf , string cnh , Localizacao endereco , DataFato cadastro  )
    {
        if(string.IsNullOrWhiteSpace(nome))
            return Result<Motorista>.Failure("Nome do motorista não pode ser vazio.");
        if(string.IsNullOrWhiteSpace(cnh))
            return Result<Motorista>.Failure("CNH do motorista não pode ser vazia.");
        return Result<Motorista>.Success( new Motorista ( nome , cpf , cnh , endereco , cadastro ) );
    }
    public Result Inativar(bool possuiFreteEmCurso )
    {
        if (this.Ativo && possuiFreteEmCurso)
        {
            return Result.Failure( "O motorista não pode ser inativado enquanto possuir frete em curso." );
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
    public void AtualizarEndereco(Localizacao endereco)
    { 
        Endereco = endereco;
    }

    public Result AtualizarNome(bool possuiFreteEmCurso, string nome )
    {
        if (this.Ativo && possuiFreteEmCurso)
        {
            return Result.Failure( "O motorista não pode ter o nome alterado enquanto possuir frete em curso." );
        }
        if(string.IsNullOrWhiteSpace(nome))
            return Result.Failure("Nome do motorista não pode ser vazio.");
        this.Nome = nome;
        return Result.Success(); 
    }
    public Result AtualizarDocumento(bool possuiFreteEmCurso, CpfCnpj cpf )
    {
        if (this.Ativo && possuiFreteEmCurso)
        {
            return Result.Failure( "O motorista não pode ter o nome alterado enquanto possuir frete em curso." );
        }
        
        this.Documento = cpf;
        return Result.Success(); 
    }
    public Result AtualizarCnh(bool possuiFreteEmCurso, string cnh )
    {
        if (this.Ativo && possuiFreteEmCurso)
        {
            return Result.Failure( "O motorista não pode ter a CNH alterada enquanto possuir frete em curso." );
        }
        if(string.IsNullOrWhiteSpace(cnh))
            return Result.Failure("CNH do motorista não pode ser vazia.");
        this.Cnh = cnh;
        return Result.Success();
    }
    public Result AtualizarEndereco(bool possuiFreteEmCurso, Localizacao endereco )
    {
        if (this.Ativo && possuiFreteEmCurso)
        {
            return Result.Failure( "O motorista não pode ter o endereço alterado enquanto possuir frete em curso." );
        }
        
        this.Endereco = endereco;
        return Result.Success();
    }
}
