using Fitshirt.Infrastructure.Models.Common.Entities;

namespace Fitshirt.Infrastructure.Repositories.Common.Entites;

public class SizeRepository : ISizeRepository
{
    public Task<IReadOnlyList<Size>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Size> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Size entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Size entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Size entity)
    {
        throw new NotImplementedException();
    }
}