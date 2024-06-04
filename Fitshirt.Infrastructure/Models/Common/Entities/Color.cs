using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Models.Posts;

namespace Fitshirt.Infrastructure.Models.Common.Entities;

public class Color : BaseModel
{
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
    
    public ICollection<Design> DesignsPrimaryColor { get; set; }
    
    public ICollection<Design> DesignsSecondaryColor { get; set; }
    
    public ICollection<Design> DesignsTertiaryColor { get; set; }
}