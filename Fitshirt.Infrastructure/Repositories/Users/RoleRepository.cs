using Fitshirt.Infrastructure.Models.Users.Entities;

namespace Fitshirt.Infrastructure.Repositories.Users;

public class RoleRepository : IRoleRepository
{
    public Task<IReadOnlyList<Role>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Role> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddAsync(Role entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Role entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Role entity)
    {
        throw new NotImplementedException();
    }
}