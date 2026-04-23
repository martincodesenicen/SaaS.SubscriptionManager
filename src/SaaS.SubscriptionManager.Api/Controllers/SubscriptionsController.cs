using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaS.SubscriptionManager.Application.Subscriptions.Commands;

namespace SaaS.SubscriptionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSubscriptionCommand command)
    {
        // Enviamos el comando a MediatR. 
        // Él buscará automáticamente al CreateSubscriptionHandler.
        var subscriptionId = await _mediator.Send(command);

        return Ok(subscriptionId);
    }
}