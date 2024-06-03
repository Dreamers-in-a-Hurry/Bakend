using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Designs.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Repositories.Designs;

public class ShieldRepository : IShieldRepository
{
    private readonly FitshirtDbContext _context;

    public ShieldRepository(FitshirtDbContext context)
    {
        _context = context;
    }
    public Task<IReadOnlyList<DesignShield>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<DesignShield?> GetByIdAsync(int id)
    {
        return await _context.Shields.Where(shield => shield.IsEnable && shield.Id == id)
            .FirstOrDefaultAsync();
    }

    public Task<bool> AddAsync(DesignShield entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, DesignShield entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}