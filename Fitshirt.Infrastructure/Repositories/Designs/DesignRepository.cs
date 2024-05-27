using Fitshirt.Infrastructure.Models.Designs;

namespace Fitshirt.Infrastructure.Repositories.Designs;

public class DesignRepository : IDesignRepository
{
    public Task<IReadOnlyList<Design>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Design> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Design entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Design entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Design entity)
    {
        throw new NotImplementedException();
    }
}