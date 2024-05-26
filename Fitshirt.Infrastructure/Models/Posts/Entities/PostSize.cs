using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Common.Entities;

namespace Fitshirt.Infrastructure.Models.Posts.Entities;

public class PostSize : BaseModel
{
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int SizeId { get; set; }
    public Size Size { get; set; }
}