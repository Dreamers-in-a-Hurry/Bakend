using System.Net;
using Fitshirt.Api.Errors;
using Fitshirt.Domain.Exceptions;
using Newtonsoft.Json;

namespace Fitshirt.Api.Extensions.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        
        context.Response.ContentType = "application/json";
        var statusCode = (int)HttpStatusCode.InternalServerError;

        switch (exception)
        {
            case NotFoundEntityAttributeException:
                statusCode = (int)HttpStatusCode.NotFound;
                break;
            case DuplicateEntityAttributeException:
                statusCode = (int)HttpStatusCode.Conflict;
                break;
            case NotFoundInListException<int>:
                statusCode = (int)HttpStatusCode.NotFound;
                break;
            case ValidationException:
                statusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                break;
        }

        var result = JsonConvert.SerializeObject(
            new CodeErrorResponse(statusCode, exception.Message)
        );

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(result);
    }

}