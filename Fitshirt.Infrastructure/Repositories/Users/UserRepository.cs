using Fitshirt.Infrastructure.Models.Users;

namespace Fitshirt.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    public Task<IReadOnlyList<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(User entity)
    {
        throw new NotImplementedException();
    }
}