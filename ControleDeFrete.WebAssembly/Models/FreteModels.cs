using System.ComponentModel.DataAnnotations;

namespace ControleDeFrete.WebAssembly.Models;

public sealed class FreteCreate
{
    [Required]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    public string DocumentoCliente { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal ValorFrete { get; set; }

    [Range(0, double.MaxValue)]
    public decimal ValorDescarga { get; set; }

    [Range(0, double.MaxValue)]
    public decimal ValorPagoMotorista { get; set; }

    [Required]
    public string Logadouro { get; set; } = string.Empty;

    [Required]
    public string Cidade { get; set; } = string.Empty;

    [Required]
    [StringLength(2, MinimumLength = 2)]
    public string Estado { get; set; } = string.Empty;

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public sealed class FreteResponse
{
    public string Codigo { get; set; } = string.Empty;
    public DateOnly DataEmissao { get; set; }
    public DateOnly? DataCarregamento { get; set; }
    public DateOnly? DataEntrega { get; set; }
    public DateOnly? DataPagamento { get; set; }
    public decimal Valor { get; set; }
    public decimal Descarga { get; set; }
    public decimal PagoMotorista { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; } = string.Empty;
    public string MotoristaNome { get; set; } = string.Empty;
    public string VeiculoPlaca { get; set; } = string.Empty;
    public string Origem { get; set; } = string.Empty;
    public string Destinos { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}

public sealed class EntregaCreate
{
    [Required]
    public string DocumentoCliente { get; set; } = string.Empty;

    [Required]
    public string Logradouro { get; set; } = string.Empty;

    [Required]
    public string Cidade { get; set; } = string.Empty;

    [Required]
    [StringLength(2, MinimumLength = 2)]
    public string Estado { get; set; } = string.Empty;

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Observacoes { get; set; }
}

public sealed class EntregaFinalizeRequest
{
    [Range(1, int.MaxValue)]
    public int Sequencia { get; set; }

    [Required]
    public DateOnly DataEntrega { get; set; } = DateOnly.FromDateTime( DateTime.UtcNow );
}
