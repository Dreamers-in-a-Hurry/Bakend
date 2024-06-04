using System.Net;
using Fitshirt.Domain.Exceptions;

namespace Fitshirt.Api.Errors;

public static class CodeErrorResponseFactory
{
    public static CodeErrorResponse CreateCodeErrorResponse(Exception exception)
    {
        var statusCode = exception switch
        {
            NotFoundEntityAttributeException => (int)HttpStatusCode.NotFound,
            NoEntitiesFoundException => (int)HttpStatusCode.NotFound,
            DuplicateEntityAttributeException => (int)HttpStatusCode.Conflict,
            NotFoundInListException<int> => (int)HttpStatusCode.NotFound,
            ValidationException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        return new CodeErrorResponse(statusCode, exception.Message);
    }
}