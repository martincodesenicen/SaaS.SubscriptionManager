using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaS.SubscriptionManager.Application.Subscriptions.Commands;
using SaaS.SubscriptionManager.Application.Subscriptions.Queries;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        // Creamos el Query con el ID que viene de la URL
        var query = new GetSubscriptionByIdQuery(id);

        // Lo enviamos por MediatR
        var result = await _mediator.Send(query);

        // Si no existe, devolvemos un 404. Si existe, el DTO con los datos
        if (result == null)
        {
            return NotFound(new { Message = $"No se encontró la suscripción con ID: {id}" });
        }

        return Ok(result);
    }

    [HttpPut("{id}/activate")]
    public async Task<IActionResult> Activate(Guid id, [FromBody] DateTime expirationDate)
    {
        var result = await _mediator.Send(new ActivateSubscriptionCommand(id, expirationDate));

        if (!result) return NotFound();

        return NoContent(); // 204: Se procesó correctamente pero no devolvemos contenido
    }
}