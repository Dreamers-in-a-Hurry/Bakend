using System.Net;
using Fitshirt.Api.Errors;
using Fitshirt.Domain.Exceptions;
using Newtonsoft.Json;

namespace Fitshirt.Api.Extensions.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
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
        var codeErrorResponse = CodeErrorResponseFactory.CreateCodeErrorResponse(exception);

        var result = JsonConvert.SerializeObject(codeErrorResponse);

        context.Response.StatusCode = codeErrorResponse.StatusCode;

        await context.Response.WriteAsync(result);
    }

}