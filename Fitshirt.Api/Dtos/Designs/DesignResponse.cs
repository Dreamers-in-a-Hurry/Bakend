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
    public ColorResponse ColorPrimary { get; set; }
    public ColorResponse ColorSeconndary { get; set; }
    public ColorResponse ColorTertiary { get; set; }
    public ShieldResponse Shield { get; set; }
    public UserResponse User { get; set; }
    
    public List<DesignShieldResponse> NameTeam { get; set; }
}