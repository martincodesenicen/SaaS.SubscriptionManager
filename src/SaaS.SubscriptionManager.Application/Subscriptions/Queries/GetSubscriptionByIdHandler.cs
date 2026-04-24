using MediatR;
using SaaS.SubscriptionManager.Domain.Interfaces;

namespace SaaS.SubscriptionManager.Application.Subscriptions.Queries;

public class GetSubscriptionByIdHandler : IRequestHandler<GetSubscriptionByIdQuery, SubscriptionResponse?>
{
    private readonly ISubscriptionRepository _repository;

    public GetSubscriptionByIdHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionResponse?> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _repository.GetByIdAsync(request.Id);

        if (subscription == null) return null;

        return new SubscriptionResponse(
            subscription.Id, 
            subscription.UserId, 
            subscription.Status.ToString(), 
            subscription.CreatedAt,
            subscription.ExpirationDate);
    }
}