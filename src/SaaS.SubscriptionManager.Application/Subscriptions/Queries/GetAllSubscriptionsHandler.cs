using MediatR;
using SaaS.SubscriptionManager.Domain.Interfaces;

namespace SaaS.SubscriptionManager.Application.Subscriptions.Queries;

public class GetAllSubscriptionsHandler : IRequestHandler<GetAllSubscriptionsQuery, List<SubscriptionResponse>>
{
    private readonly ISubscriptionRepository _repository;

    public GetAllSubscriptionsHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SubscriptionResponse>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await _repository.GetAllAsync(); // Asegúrate de que tu repo tenga este método
        
        return subscriptions.Select(s => new SubscriptionResponse(
            s.Id,
            s.UserId,
            s.Status.ToString(),
            s.CreatedAt,
            s.ExpirationDate
        )).ToList();
    }
}