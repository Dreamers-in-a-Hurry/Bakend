using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Repositories.Common.Entites;

public class ColorRepository : IColorRepository
{
    private readonly FitshirtDbContext _context;

    public ColorRepository(FitshirtDbContext context)
    {
        _context = context;
    }

    public Task<IReadOnlyList<Color>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Color?> GetByIdAsync(int id)
    {
        return await _context.Colors.Where(post => post.IsEnable && post.Id == id).FirstOrDefaultAsync();
    }

    public Task<bool> AddAsync(Color entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, Color entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}