using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Posts;

namespace Fitshirt.Infrastructure.Models.Purchases.Entities;

public class Category : BaseModel
{
    public string Name { get; set; }
    
    public ICollection<Post> Posts { get; set; }
}