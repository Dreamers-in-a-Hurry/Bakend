using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Posts.Entities;

namespace Fitshirt.Infrastructure.Models.Purchases.Entities;

public class Item : BaseModel
{
    public int Quantity { get; set; }
    
    public int SizeId { get; set; }
    public Size Size { get; set; }
    
    public int UserId { get; set; }
    public int PurchaseId { get; set; }
}