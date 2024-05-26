using Fitshirt.Infrastructure.Models.Common.Entities;

namespace Fitshirt.Infrastructure.Repositories.Common.Entites;

public class ColorRepository : IColorRepository
{
    public Task<IReadOnlyList<Color>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Color> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddAsync(Color entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Color entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Color entity)
    {
        throw new NotImplementedException();
    }
}