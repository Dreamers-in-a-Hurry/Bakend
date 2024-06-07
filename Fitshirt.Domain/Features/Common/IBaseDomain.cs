using Fitshirt.Infrastructure.Models.Common;

namespace Fitshirt.Domain.Features.Common;

public interface IBaseDomain<T> where T : BaseModel
{
    Task<Boolean> AddAsync(T entity);
    Task<Boolean> UpdateAsync(int id, T entity);
    Task<Boolean> DeleteAsync(int id);
}