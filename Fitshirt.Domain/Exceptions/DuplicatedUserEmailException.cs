namespace Fitshirt.Domain.Exceptions;

public class DuplicatedUserEmailException : DuplicateEntityAttributeException
{
    public DuplicatedUserEmailException(object attributeValue) 
        : base("User", "Email", attributeValue)
    {
    }
}