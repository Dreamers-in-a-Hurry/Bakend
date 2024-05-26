using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Purchases.Entities;

namespace Fitshirt.Infrastructure.Models.Posts.Entities;

public class Size : BaseModel
{
    public string Value { get; set; }

    public ICollection<Post> Posts { get; set; }
    public ICollection<Item> Items { get; set; }
}