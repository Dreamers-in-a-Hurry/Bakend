using Fitshirt.Infrastructure.Models.Common;

namespace Fitshirt.Infrastructure.Models.Posts.Entities;

public class Category : BaseModel
{
    public string Name { get; set; }
    
    public ICollection<Post> Posts { get; set; }
}