using Fitshirt.Domain.Features.Common;
using Fitshirt.Infrastructure.Models.Posts;

namespace Fitshirt.Domain.Features.Posts;

public interface IPostDomain : IBaseDomain<Post>
{
    Task<bool> AddPostAsync(Post post, ICollection<int> sizeIds);
    Task<bool> UpdatePostAsync(int id, Post post, ICollection<int> sizeIds);
}