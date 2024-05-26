using Fitshirt.Infrastructure.Models.Posts;

namespace Fitshirt.Infrastructure.Repositories.Posts;

public class PostRepository : IPostRepository
{
    public Task<IReadOnlyList<Post>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Post entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Post entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Post entity)
    {
        throw new NotImplementedException();
    }
}