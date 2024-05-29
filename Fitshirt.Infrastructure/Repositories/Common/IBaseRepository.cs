using Fitshirt.Infrastructure.Models.Common;

namespace Fitshirt.Infrastructure.Repositories.Common;

public interface IBaseRepository<T> where T : BaseModel
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateAsync(int id, T entity);
    Task<bool> DeleteAsync(int id);
}