using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly FitshirtDbContext _context;

    public UserRepository(FitshirtDbContext context)
    {
        _context = context;
    }

    public Task<IReadOnlyList<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.Where(user => user.IsEnable && user.Id == id).FirstOrDefaultAsync();
    }

    public Task<bool> AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}