using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Repositories.Common;

namespace Fitshirt.Infrastructure.Repositories.Designs;

public interface IDesignRepository : IBaseRepository<Design>
{
    Task<IReadOnlyCollection<Design>> GetDesignsByUserId(int userId);
}