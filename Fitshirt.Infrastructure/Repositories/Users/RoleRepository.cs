using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Repositories.Users;

public class RoleRepository : IRoleRepository
{
    private readonly FitshirtDbContext _context;

    public RoleRepository(FitshirtDbContext context)
    {
        _context = context;
    }

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

    public async Task<Role?> GetClientRoleAsync()
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Client");
    }

    public async Task<Role?> GetAdminRoleAsync()
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
    }
}