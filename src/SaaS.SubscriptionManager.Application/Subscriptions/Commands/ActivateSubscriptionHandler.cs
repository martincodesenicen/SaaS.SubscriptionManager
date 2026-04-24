using MediatR;
using SaaS.SubscriptionManager.Domain.Interfaces;

namespace SaaS.SubscriptionManager.Application.Subscriptions.Commands;

public class ActivateSubscriptionHandler : IRequestHandler<ActivateSubscriptionCommand, bool>
{
    private readonly ISubscriptionRepository _repository;

    public ActivateSubscriptionHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(ActivateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        // Buscamos la sub
        var subscription = await _repository.GetByIdAsync(request.Id);

        if (subscription == null) return false;

        // Ejecutamos la lógica de negocio
        subscription.Activate(request.ExpirationDate);

        // Guardamos los cambios
        await _repository.UpdateAsync(subscription);

        return true;
    }
}