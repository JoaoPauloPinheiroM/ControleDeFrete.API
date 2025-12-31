using ControleDeFrete.API.Application.Common.Result;

namespace ControleDeFrete.API.Domain.ValueObjects;

public readonly struct Localizacao
{
    public string Logradouro { get; }
    public string Cidade { get; }
    public string Estado { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    private Localizacao ( string lodagouro , string cidade , string estado , double latitude , double longitude )
    {
        Logradouro = lodagouro;
        Cidade = cidade;
        Estado = estado;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Result<Localizacao> Create ( string logradouro , string cidade , string estado , double lat , double lon )
    {
        if (string.IsNullOrWhiteSpace( logradouro )) return "Logradouro é obrigatório."; 
        if (string.IsNullOrWhiteSpace( cidade )) return "Cidade é obrigatória."; 
        

        var estadoNormalizado = estado?.Trim().ToUpper();
        if (string.IsNullOrWhiteSpace( estadoNormalizado ) || estadoNormalizado.Length != 2)
            return Result<Localizacao>.Failure( "Estado deve conter exatamente 2 caracteres." );

        //30-12-2025: joão.mourao -  Validação técnica de coordenadas
        if (lat < -90 || lat > 90) return "Latitude inválida."; 
        if (lon < -180 || lon > 180) return "Longitude inválida."; 

        return Result<Localizacao>.Success( new Localizacao( logradouro , cidade , estadoNormalizado! , lat , lon ) ); 
    }

    
}
