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
    public async Task<IReadOnlyList<Shield>> GetAllAsync()
    {
        return await _context.Shields.Where(shield => shield.IsEnable).ToListAsync();
    }

    public async Task<Shield?> GetByIdAsync(int id)
    {
        return await _context.Shields.Where(shield => shield.IsEnable && shield.Id == id)
            .FirstOrDefaultAsync();
    }

    public Task<bool> AddAsync(Shield entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, Shield entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}