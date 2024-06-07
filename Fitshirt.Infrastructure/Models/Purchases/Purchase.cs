using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Purchases.Entities;
using Fitshirt.Infrastructure.Models.Users;

namespace Fitshirt.Infrastructure.Models.Purchases;

public sealed class Purchase : BaseModel
{
    public DateTime PurchaseDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<Item> Items { get; set; }
}