namespace Fitshirt.Domain.Exceptions;

public class NotFoundInListException<T> : Exception
{
    public NotFoundInListException(string name, object key, List<T> missingItems)
        : base($"Entity \"{name}\" with attribute list: {key}: [{string.Join(", ", missingItems)}] were not found.")
    {
    }
}