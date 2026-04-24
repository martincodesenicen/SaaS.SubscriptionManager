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
    }
}