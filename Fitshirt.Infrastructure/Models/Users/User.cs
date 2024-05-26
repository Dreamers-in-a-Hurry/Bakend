using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Models.Purchases;
using Fitshirt.Infrastructure.Models.Purchases.Entities;
using Fitshirt.Infrastructure.Models.Users.Entities;
using Fitshirt.Infrastructure.Models.Users.ValueObjects;

namespace Fitshirt.Infrastructure.Models.Users;

public sealed class User : BaseModel
{
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }
    public DateOnly BirthDate { get; set; }
    public Address? Address { get; set; }
    public DebitCard? DebitCard { get; set; }
    
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    
    public ICollection<Design> Designs { get; set; }
    public ICollection<Purchase> Purchases { get; set; }
    public ICollection<Item> Items { get; set; }
}