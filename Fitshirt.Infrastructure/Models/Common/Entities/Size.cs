using Fitshirt.Infrastructure.Models.Posts.Entities;
using Fitshirt.Infrastructure.Models.Purchases.Entities;

namespace Fitshirt.Infrastructure.Models.Common.Entities;

public class Size : BaseModel
{
    public string Value { get; set; }

    public ICollection<PostSize> Posts { get; set; }
    public ICollection<Item> Items { get; set; }
}