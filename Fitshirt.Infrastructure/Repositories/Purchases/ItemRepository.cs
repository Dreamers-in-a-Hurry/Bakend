using Fitshirt.Infrastructure.Models.Purchases.Entities;

namespace Fitshirt.Infrastructure.Repositories.Purchases;

public class ItemRepository : IItemRepository
{
    public Task<IReadOnlyList<Item>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Item?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Item entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, Item entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}