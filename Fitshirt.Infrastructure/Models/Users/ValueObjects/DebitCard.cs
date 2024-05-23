namespace Fitshirt.Infrastructure.Models.Users.ValueObjects;

public class DebitCard
{
    public string CardNumber { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public string CVV { get; set; }
}