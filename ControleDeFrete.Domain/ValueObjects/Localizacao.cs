using ControleDeFrete.Domain.Common;

namespace ControleDeFrete.Domain.ValueObjects;

public sealed class Localizacao
{
    public string Logradouro { get; }
    public string Cidade { get; }
    public string Estado { get; }
    public double? Latitude { get; }
    public double? Longitude { get; }

    private Localizacao ( string logradouro , string cidade , string estado , double? latitude , double? longitude )
    {
        Logradouro = logradouro;
        Cidade = cidade;
        Estado = estado;
        Latitude = latitude;
        Longitude = longitude;
    
    }

    public static Result<Localizacao> Create ( string logradouro , string cidade , string estado , double? latitude , double? longitude )
    {
        if (string.IsNullOrWhiteSpace( logradouro )) return "Logradouro é obrigatório."; 
        if (string.IsNullOrWhiteSpace( cidade )) return "Cidade é obrigatória."; 
        

        var estadoNormalizado = estado?.Trim().ToUpper();
        if (string.IsNullOrWhiteSpace( estadoNormalizado ) || estadoNormalizado.Length != 2)
            return Result<Localizacao>.Failure( "Estado deve conter exatamente 2 caracteres." );

        //30-12-2025: joão.mourao -  Validação técnica de coordenadas
        if (latitude < -90 || latitude > 90) return "Latitude inválida."; 
        if (longitude < -180 || longitude > 180) return "Longitude inválida."; 

        return Result<Localizacao>.Success( new Localizacao( logradouro , cidade , estadoNormalizado! , latitude , longitude ) ); 
    }

    
}
