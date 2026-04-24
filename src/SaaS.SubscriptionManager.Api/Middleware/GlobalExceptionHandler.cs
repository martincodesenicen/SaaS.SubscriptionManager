using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SaaS.SubscriptionManager.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        // Logueamos el error para tener rastro en el servidor
        _logger.LogError(exception, "Ocurrió un error no controlado: {Message}", exception.Message);

        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            // Errores de entrada de datos
            case ValidationException validationException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Validation Error";
                problemDetails.Detail = "Se encontraron errores de validación.";
                problemDetails.Extensions["errors"] = validationException.Errors.Select(e => e.ErrorMessage);
                break;

            // Errores de lógica de negocio (Dominio)
            case ArgumentException argumentException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Bad Request";
                problemDetails.Detail = argumentException.Message; // Aquí saldrá tu mensaje: "[!] La fecha de expiración..."
                break;

            // 3. Error genérico (500)
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Server Error";
                problemDetails.Detail = "Ocurrió un error interno en el servidor.";
                break;
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}