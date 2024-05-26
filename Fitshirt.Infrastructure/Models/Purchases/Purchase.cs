using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Users;

namespace Fitshirt.Infrastructure.Models.Purchases;

public sealed class Purchase : BaseModel
{
    public DateTime PurchaseDate { get; set; }
    
    public ICollection<User> Users { get; set; }
}