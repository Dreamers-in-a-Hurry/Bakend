using Fitshirt.Infrastructure.Models.Common;

namespace Fitshirt.Infrastructure.Models.Designs.Entities;

public class Shield : BaseModel
{
    public string NameTeam { get; set; }
    
    public ICollection<Design> Designs { get; set; }
}