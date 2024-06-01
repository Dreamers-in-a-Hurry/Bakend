namespace Fitshirt.Domain.Exceptions;

public class NotFoundEntityAttributeException : Exception
{
    protected NotFoundEntityAttributeException(string entityName, string attributeName, object attributeValue)
        : base($"Entity '{entityName}' with attribute ({attributeName}: {attributeValue}) was not found")
    {
        
    }
}