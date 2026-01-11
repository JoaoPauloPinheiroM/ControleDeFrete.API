namespace ControleDeFrete.Domain.Common;


public readonly struct Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public bool IsFailure => !IsSuccess;

    private Result ( bool success , string? error )
    {
        IsSuccess = success;
        Error = error;
    }

    public static Result Success ( ) => new( true , null );
    public static Result Failure ( string error ) => new( false , error );
}


public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }
    public bool IsFailure => !IsSuccess;

    private Result ( bool success , T? value , string? error )
    {
        IsSuccess = success;
        Value = value;
        Error = error;
    }

    public static Result<T> Success ( T value ) => new( true , value , null );
    public static Result<T> Failure ( string error ) => new( false , default , error );

    public static implicit operator Result<T> ( string error ) => Failure( error );
}
