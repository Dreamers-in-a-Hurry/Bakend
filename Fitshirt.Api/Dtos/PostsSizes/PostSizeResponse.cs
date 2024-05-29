using Fitshirt.Api.Dtos.Sizes;

namespace Fitshirt.Api.Dtos.PostsSizes;

public class PostSizeResponse
{
    public int SizeId { get; set; }
    public SizeResponse Sizes { get; set; }
}