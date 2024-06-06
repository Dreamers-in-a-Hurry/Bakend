namespace Fitshirt.Domain.Exceptions;

public class DuplicateEntityAttributeException : Exception
{
    public string EntityName { get; }
    public string AttributeName { get; }
    public object AttributeValue { get; }
    
    protected DuplicateEntityAttributeException(string entityName, string attributeName, object attributeValue)
        :base($"Entity '{entityName}' with attribute '{attributeName}': '{attributeValue}' is already registered.")
    {
        
        EntityName = entityName;
        AttributeName = attributeName;
        AttributeValue = attributeValue;
    }
}