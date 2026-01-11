namespace ControleDeFrete.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{  
        public Task<bool> CommitAsync ( CancellationToken cancellationToken = default );
    
}
