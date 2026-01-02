namespace ControleDeFrete.API.Domain.Interfaces;

public interface IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        
        Task<bool> CommitAsync ( CancellationToken cancellationToken = default );
    }
}
