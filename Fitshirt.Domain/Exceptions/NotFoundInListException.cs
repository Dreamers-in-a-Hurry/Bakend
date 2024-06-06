namespace Fitshirt.Domain.Exceptions;

public class NotFoundInListException<T> : Exception
{
    public string Name { get; }
    public object Key { get; }
    public List<T> MissingItems { get; }
    
    public NotFoundInListException(string name, object key, List<T> missingItems)
        : base($"Entity \"{name}\" with attribute list: {key}: [{string.Join(", ", missingItems)}] were not found.")
    {
        Name = name;
        Key = key;
        MissingItems = missingItems;
    }
}