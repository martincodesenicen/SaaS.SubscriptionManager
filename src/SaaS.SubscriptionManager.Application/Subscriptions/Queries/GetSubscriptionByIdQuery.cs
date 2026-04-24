using MediatR;

namespace SaaS.SubscriptionManager.Application.Subscriptions.Queries;

public record GetSubscriptionByIdQuery(Guid Id) : IRequest<SubscriptionResponse?>;

// Este es el objeto que devolveremos (DTO) para no exponer la entidad real
public record SubscriptionResponse(Guid Id, Guid UserId, string Status, DateTime CreatedAt, DateTime? ExpirationDate);