using MediatR;

namespace SaaS.SubscriptionManager.Application.Subscriptions.Commands;

// Pasamos el ID de la sub y la fecha en la que queremos que expire
public record ActivateSubscriptionCommand(Guid Id, DateTime ExpirationDate) : IRequest<bool>;