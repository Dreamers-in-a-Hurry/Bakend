using Fitshirt.Infrastructure.Models.Common;

namespace Fitshirt.Infrastructure.Models.Users.Entities;

public class DebitCard : BaseModel
{
    public string CardNumber { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public string CVV { get; set; }
    
    public User User { get; set; }
}