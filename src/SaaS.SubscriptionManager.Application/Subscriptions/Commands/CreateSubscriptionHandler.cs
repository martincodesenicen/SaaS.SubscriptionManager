using MediatR;
using SaaS.SubscriptionManager.Domain.Entities;
using SaaS.SubscriptionManager.Domain.Interfaces;

namespace SaaS.SubscriptionManager.Application.Subscriptions.Commands;

// Este es el encargado de procesar el CreateSubscriptionCommand
public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionCommand, Guid>
{
    private readonly ISubscriptionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    // Inyectamos las interfaces del Dominio (Desacoplamiento total)
    public CreateSubscriptionHandler(ISubscriptionRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        // 1. Lógica de negocio: Creamos la entidad
        // Nota: El UserId viene del Command que enviaremos desde la API
        var subscription = new Subscription(request.UserId);

        // 2. Persistencia: Agregamos al repositorio (en memoria de EF por ahora)
        await _repository.AddAsync(subscription);

        // 3. Confirmación: Guardamos cambios en la BD real
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 4. Retornamos el ID para que la API sepa qué se creó
        return subscription.Id;
    }
}