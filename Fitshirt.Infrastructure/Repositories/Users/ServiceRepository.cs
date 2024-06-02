using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Repositories.Users;

public class ServiceRepository : IServiceRepository
{
    private readonly FitshirtDbContext _context;

    public ServiceRepository(FitshirtDbContext context)
    {
        _context = context;
    }

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

    public async Task<Service?> GetFreeServiceAsync()
    {
        return await _context.Services.FirstOrDefaultAsync(s => s.Name == "Premium");
    }

    public async Task<Service?> GetPremiumServiceAsync()
    {
        return await _context.Services.FirstOrDefaultAsync(s => s.Name == "Free");
    }
}