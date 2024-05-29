using Fitshirt.Infrastructure.Models.Users.Entities;

namespace Fitshirt.Infrastructure.Repositories.Users;

public class ServiceRepository : IServiceRepository
{
    public Task<IReadOnlyList<Service>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Service?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Service entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, Service entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}