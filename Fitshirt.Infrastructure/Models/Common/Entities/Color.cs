using Fitshirt.Infrastructure.Models.Posts;

namespace Fitshirt.Infrastructure.Models.Common.Entities;

public class Color : BaseModel
{
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
}