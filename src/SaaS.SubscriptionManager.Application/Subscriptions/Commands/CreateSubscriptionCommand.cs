using MediatR;
using SaaS.SubscriptionManager.Domain.Entities;
using SaaS.SubscriptionManager.Domain.Interfaces;
namespace SaaS.SubscriptionManager.Applcation.Subscriptions.Commands;
public record CreateSubscriptionCommand(Guid UserId) : IRequest<Guid>;
public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionCommand, Guid>
{
    private readonly ISubscriptionRepository _repository;
    public CreateSubscriptionHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }
    public async Task<Guid> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = new Subscription(request.UserId); // creamos la entidad
        await _repository.AddAsync(subscription); // la guardamos en el repo
        return subscription.Id; // retornamos id de la nueva sub
    }
}
