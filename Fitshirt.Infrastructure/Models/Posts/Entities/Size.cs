using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Posts;

namespace Fitshirt.Infrastructure.Models.Purchases.Entities;

public class Size : BaseModel
{
    public string Value { get; set; }

    public ICollection<Post> Posts { get; set; }
}