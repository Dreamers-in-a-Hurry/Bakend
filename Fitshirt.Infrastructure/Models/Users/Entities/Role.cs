using Fitshirt.Infrastructure.Models.Common;

namespace Fitshirt.Infrastructure.Models.Users.Entities;

public class Role : BaseModel
{
    public string Name { get; set; }
    public ICollection<User> Users { get; set; }
}