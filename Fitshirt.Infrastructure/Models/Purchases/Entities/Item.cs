using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Posts;

namespace Fitshirt.Infrastructure.Models.Purchases.Entities;

public class Item : BaseModel
{
    public int Quantity { get; set; }
    
    public int SizeId { get; set; }
    public Size Size { get; set; }
    
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; }
}