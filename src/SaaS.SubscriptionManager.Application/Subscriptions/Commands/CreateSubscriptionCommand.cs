using MediatR;
using SaaS.SubscriptionManager.Domain.Entities;
using SaaS.SubscriptionManager.Domain.Interfaces;
namespace SaaS.SubscriptionManager.Application.Subscriptions.Commands;
// el command contiene solo los datos necesarios para la operacion
public record CreateSubscriptionCommand(Guid UserId) : IRequest<Guid>;
// el handler contiene la lógica de qué pasa cuando ejecutamos ee comando
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
