using Fitshirt.Api.Dtos.Categories;
using Fitshirt.Api.Dtos.Colors;
using Fitshirt.Api.Dtos.PostsSizes;
using Fitshirt.Api.Dtos.Users;

namespace Fitshirt.Api.Dtos.Posts;

public class PostResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public CategoryResponse Category { get; set; }
    public ColorResponse Color { get; set; }
    public UserResponse User { get; set; }
    public List<PostSizeResponse> Sizes { get; set; }
}