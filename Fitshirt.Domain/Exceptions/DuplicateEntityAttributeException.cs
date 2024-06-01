namespace Fitshirt.Domain.Exceptions;

public class DuplicateEntityAttributeException : Exception
{
    protected DuplicateEntityAttributeException(string entityName, string attributeName, object attributeValue)
        :base($"Entity '{entityName}' with attribute '{attributeName}': '{attributeValue}' is already registered.")
    {
    }
}