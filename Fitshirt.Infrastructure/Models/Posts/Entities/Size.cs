using Fitshirt.Infrastructure.Models.Common;

namespace Fitshirt.Infrastructure.Models.Posts.Entities;

public class Size : BaseModel
{
    public string Value { get; set; }

    public ICollection<Post> Posts { get; set; }
}