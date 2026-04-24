using MediatR;

namespace SaaS.SubscriptionManager.Application.Subscriptions.Queries;

public record GetAllSubscriptionsQuery() : IRequest<List<SubscriptionResponse>>;