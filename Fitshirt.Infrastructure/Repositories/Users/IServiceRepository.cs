using Fitshirt.Infrastructure.Models.Users.Entities;
using Fitshirt.Infrastructure.Repositories.Common;

namespace Fitshirt.Infrastructure.Repositories.Users;

public interface IServiceRepository : IBaseRepository<Service>
{
    Task<Service?> GetFreeServiceAsync();
    Task<Service?> GetPremiumServiceAsync();
}