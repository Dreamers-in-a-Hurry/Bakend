using Fitshirt.Infrastructure.Models.Common;

namespace Fitshirt.Infrastructure.Models.Users.Entities;

public class Service : BaseModel
{
    public string Name { get; set; }
    
    public ICollection<User> Users { get; set; }
}