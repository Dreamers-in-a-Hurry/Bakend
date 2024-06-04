using Fitshirt.Api.Dtos.Categories;
using Fitshirt.Api.Dtos.Colors;
using Fitshirt.Api.Dtos.DesignShields;
using Fitshirt.Api.Dtos.Shields;
using Fitshirt.Api.Dtos.Users;

namespace Fitshirt.Api.Dtos.Designs;

public class DesignResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ColorResponse PrimaryColor { get; set; }
    public ColorResponse SecondaryColor { get; set; }
    public ColorResponse TertiaryColor { get; set; }
    public ShieldResponse Shield { get; set; }
    public UserResponse User { get; set; }
    
    
}