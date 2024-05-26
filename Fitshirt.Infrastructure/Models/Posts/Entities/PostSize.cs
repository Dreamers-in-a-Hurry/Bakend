using Fitshirt.Infrastructure.Models.Common;

namespace Fitshirt.Infrastructure.Models.Posts.Entities;

public class PostSize : BaseModel
{
    public int PostId { get; set; }
    public int SizeId { get; set; }
}