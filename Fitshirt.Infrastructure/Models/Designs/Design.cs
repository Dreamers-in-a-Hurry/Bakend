using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Designs.Entities;
using Fitshirt.Infrastructure.Models.Users;

namespace Fitshirt.Infrastructure.Models.Designs;

public sealed class Design : BaseModel
{
    public string Name { get; set; }
    public string? Image { get; set; }
    
    public int ShieldId { get; set; }
    public Shield Shield { get; set; }
    
    public int PrimaryColorId { get; set; }
    public Color PrimaryColor { get; set; }
    public int SecondaryColorId { get; set; }
    public Color SecondaryColor { get; set; }
    public int TertiaryColorId { get; set; }
    public Color TertiaryColor { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}