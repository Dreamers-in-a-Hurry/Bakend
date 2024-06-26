using Fitshirt.Infrastructure.Models.Common.Entities;

namespace Fitshirt.Infrastructure.Repositories.Common.Entites;

public interface ISizeRepository : IBaseRepository<Size>
{
    Task<ICollection<Size>> GetSizesByIdsAsync(ICollection<int> sizeIds);
}