using Fitshirt.Infrastructure.Models.Purchases;

namespace Fitshirt.Infrastructure.Repositories.Purchases;

public class PurchaseRepository : IPurchaseRepository
{
    public Task<IReadOnlyList<Purchase>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Purchase> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Purchase entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Purchase entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Purchase entity)
    {
        throw new NotImplementedException();
    }
}