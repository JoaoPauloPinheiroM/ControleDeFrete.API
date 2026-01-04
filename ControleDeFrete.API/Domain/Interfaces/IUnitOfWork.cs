namespace ControleDeFrete.API.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{  
        public Task<bool> CommitAsync ( CancellationToken cancellationToken = default );
    
}
