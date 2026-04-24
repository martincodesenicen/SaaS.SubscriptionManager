using FluentValidation;
using SaaS.SubscriptionManager.Application.Subscriptions.Commands;

namespace SaaS.SubscriptionManager.Application.Subscriptions.Validators;

public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        // Regla: El UserId no puede ser el Guid vacío (00000000-...)
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("El ID de usuario es obligatorio y no puede estar vacío.");

        // Ejemplo de otra regla si tuvieras más campos:
        // RuleFor(x => x.PlanId).GreaterThan(0).WithMessage("Debe seleccionar un plan válido.");
    }
}