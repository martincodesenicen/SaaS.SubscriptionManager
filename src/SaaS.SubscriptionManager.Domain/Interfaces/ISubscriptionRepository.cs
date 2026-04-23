using SaaS.SubscriptionManager.Domain.Entities;
namespace SaaS.SubscriptionManager.Domain.Interfaces;
public interface ISubscriptionRepository
{
    // Obtener una sub por ID
    Task<Subscription?> GetByIdAsync(Guid id);
    // Guardar o actualizar una sub
    Task AddAsync (Subscription subscription);
    Task UpdateAsync(Subscription subscription);
}