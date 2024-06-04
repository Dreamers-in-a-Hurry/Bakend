namespace Fitshirt.Domain.Exceptions;

public class DuplicatedUserUsernameException : DuplicateEntityAttributeException
{
    public DuplicatedUserUsernameException(object attributeValue) 
        : base("User", "Username", attributeValue)
    {
    }
}