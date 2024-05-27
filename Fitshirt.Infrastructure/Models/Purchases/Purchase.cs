using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Purchases.Entities;

namespace Fitshirt.Infrastructure.Models.Purchases;

public sealed class Purchase : BaseModel
{
    public DateTime PurchaseDate { get; set; }
    
    public ICollection<Item> Items { get; set; }
}