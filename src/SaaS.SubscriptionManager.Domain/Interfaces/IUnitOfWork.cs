namespace SaaS.SubscriptionManager.Domain.Interfaces;

public interface IUnitOfWork
{
    // confirma todos los cambios en la BD
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}