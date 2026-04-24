using SaaS.SubscriptionManager.Domain.Enums;
namespace SaaS.SubscriptionManager.Domain.Entities;
public class Subscription
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public SubscriptionStatus Status { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Constructor para la creación de la primera sub
    public Subscription(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Status = SubscriptionStatus.Pending; // Se crea pendiente
        CreatedAt = DateTime.UtcNow;
    }
    public void Activate(DateTime expiration)
    {
        if(expiration <= DateTime.UtcNow) throw new ArgumentException("[!] La fecha de expiración debe ser en el futuro.");
        if(Status == SubscriptionStatus.Active) return;
        Status = SubscriptionStatus.Active;
        ExpirationDate = expiration;
    }
    public void Cancel()
    {
        if(Status == SubscriptionStatus.Canceled) return;
        Status = SubscriptionStatus.Canceled;
    }
}