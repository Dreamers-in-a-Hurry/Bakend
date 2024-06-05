using Fitshirt.Api.Dtos.Posts;
using Fitshirt.Api.Dtos.Sizes;

namespace Fitshirt.Api.Dtos.Items;

public class ItemResponse
{
    public ShirtVm Post { get; set; }
    public SizeResponse Size { get; set; }
    public int Quantity { get; set; }
}