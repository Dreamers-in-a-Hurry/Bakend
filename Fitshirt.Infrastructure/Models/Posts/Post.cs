using System.Collections;
using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Posts.Entities;
using Fitshirt.Infrastructure.Models.Purchases.Entities;
using Fitshirt.Infrastructure.Models.Users;

namespace Fitshirt.Infrastructure.Models.Posts;

public sealed class Post : BaseModel
{
    public string Name { get; set; }
    public string Image { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public int ColorId { get; set; }
    public Color Color { get; set; }
    
    public ICollection<PostSize> PostSizes { get; set; }
    public ICollection<Item> Items { get; set; } = new List<Item>();
    
    public int UserId { get; set; }
    public User User { get; set; }
}