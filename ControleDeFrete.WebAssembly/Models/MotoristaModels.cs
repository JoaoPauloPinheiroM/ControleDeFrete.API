using System.ComponentModel.DataAnnotations;

namespace ControleDeFrete.WebAssembly.Models;

public sealed class MotoristaCreate
{
    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public string Documento { get; set; } = string.Empty;

    [Required]
    public string Cnh { get; set; } = string.Empty;

    [Required]
    public string Logradouro { get; set; } = string.Empty;

    [Required]
    public string Cidade { get; set; } = string.Empty;

    [Required]
    [StringLength(2, MinimumLength = 2)]
    public string Estado { get; set; } = string.Empty;
}

public sealed class MotoristaResponse
{
    public string Nome { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string Cnh { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}

public sealed class MotoristaUpdate
{
    public string? Nome { get; set; }
    public string? Cnh { get; set; }
    public string? NovoDocumento { get; set; }
    public string? Logradouro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
}
