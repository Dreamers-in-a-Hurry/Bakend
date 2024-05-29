namespace Fitshirt.Domain.Exceptions;

public class AnyNotFoundException : ApplicationException
{
    public AnyNotFoundException(string name)
        : base($"Any \"{name}\" was not found in the list")
    {
        
    }
}