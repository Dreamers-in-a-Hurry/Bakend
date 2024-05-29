using Fitshirt.Infrastructure.Models.Posts;
using Fitshirt.Infrastructure.Repositories.Common;

namespace Fitshirt.Infrastructure.Repositories.Posts;

public interface IPostRepository : IBaseRepository<Post>
{
    Task<IReadOnlyCollection<Post>> GetPostsByUserId(int userId);
    Task<IReadOnlyCollection<Post>> SearchByFiltersAsync (int? categoryId, int? colorId);
}