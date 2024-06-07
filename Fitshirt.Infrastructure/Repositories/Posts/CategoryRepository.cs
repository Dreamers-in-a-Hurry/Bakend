using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Posts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Repositories.Posts;

public class CategoryRepository : ICategoryRepository
{
    private readonly FitshirtDbContext _context;

    public CategoryRepository(FitshirtDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Category>> GetAllAsync()
    {
        return await _context.Categories.Where(category => category.IsEnable).ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.Where(category => category.IsEnable && category.Id == id)
            .FirstOrDefaultAsync();
    }

    public Task<bool> AddAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}