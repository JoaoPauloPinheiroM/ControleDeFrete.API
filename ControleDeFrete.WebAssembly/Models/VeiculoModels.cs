using System.ComponentModel.DataAnnotations;

namespace ControleDeFrete.WebAssembly.Models;

public sealed class VeiculoCreate
{
    [Required]
    public string Placa { get; set; } = string.Empty;

    [Required]
    public string Modelo { get; set; } = string.Empty;

    [Required]
    public string Marca { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int AnoFabricacao { get; set; } = DateTime.UtcNow.Year;
}

public sealed class VeiculoResponse
{
    public string Placa { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public int AnoFabricacao { get; set; }
    public bool Ativo { get; set; }
}

public sealed class VeiculoUpdate
{
    public string? Modelo { get; set; }
    public string? Marca { get; set; }
    public int? AnoDeFabricacao { get; set; }
    public string? NovoPlaca { get; set; }
}
