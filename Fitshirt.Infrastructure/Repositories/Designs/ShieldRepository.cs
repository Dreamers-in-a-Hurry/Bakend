using Fitshirt.Infrastructure.Models.Designs.Entities;

namespace Fitshirt.Infrastructure.Repositories.Designs;

public class ShieldRepository : IShieldRepository
{
    public Task<IReadOnlyList<Shield>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Shield> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Shield entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Shield entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Shield entity)
    {
        throw new NotImplementedException();
    }
}