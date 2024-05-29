using Fitshirt.Infrastructure.Models.Users.Entities;

namespace Fitshirt.Infrastructure.Repositories.Users;

public class RoleRepository : IRoleRepository
{
    public Task<IReadOnlyList<Role>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Role?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Role entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, Role entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}