using Fitshirt.Infrastructure.Models.Users.Entities;
using Fitshirt.Infrastructure.Repositories.Common;

namespace Fitshirt.Infrastructure.Repositories.Users;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role?> GetClientRoleAsync();
    Task<Role?> GetAdminRoleAsync();
}