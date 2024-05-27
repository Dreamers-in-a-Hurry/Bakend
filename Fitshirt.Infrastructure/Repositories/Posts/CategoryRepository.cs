using Fitshirt.Infrastructure.Models.Posts.Entities;

namespace Fitshirt.Infrastructure.Repositories.Posts;

public class CategoryRepository : ICategoryRepository
{
    public Task<IReadOnlyList<Category>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Category entity)
    {
        throw new NotImplementedException();
    }
}