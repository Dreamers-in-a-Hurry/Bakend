namespace Fitshirt.Domain.Exceptions;

public class NoEntitiesFoundException : Exception
{
    public NoEntitiesFoundException(object entityName)
        : base($"No '{entityName}' found")
    {
    }
        
}