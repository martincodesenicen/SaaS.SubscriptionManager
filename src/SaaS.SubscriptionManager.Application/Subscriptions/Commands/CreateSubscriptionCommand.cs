using MediatR;

namespace SaaS.SubscriptionManager.Application.Subscriptions.Commands;

public record CreateSubscriptionCommand(Guid UserId) : IRequest<Guid>;