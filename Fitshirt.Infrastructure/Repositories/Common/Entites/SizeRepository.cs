using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Repositories.Common.Entites;

public class SizeRepository : ISizeRepository
{
    private readonly FitshirtDbContext _context;

    public SizeRepository(FitshirtDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Size>> GetAllAsync()
    {
        return await _context.Sizes.Where(size => size.IsEnable).ToListAsync();
    }

    public async Task<Size?> GetByIdAsync(int id)
    {
        return await _context.Sizes.Where(size => size.IsEnable && size.Id == id).FirstOrDefaultAsync();
    }

    public Task<bool> AddAsync(Size entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, Size entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Size>> GetSizesByIdsAsync(ICollection<int> sizeIds)
    {
        return await _context.Sizes.Where(s => sizeIds.Contains(s.Id)).ToListAsync();
    }
}