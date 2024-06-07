namespace Fitshirt.Domain.Exceptions;

public class DuplicatedUserCellphoneException : DuplicateEntityAttributeException
{
    public DuplicatedUserCellphoneException(object attributeValue) 
        : base("User", "Cellphone", attributeValue)
    {
    }
}