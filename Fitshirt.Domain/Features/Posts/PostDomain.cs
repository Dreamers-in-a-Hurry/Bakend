using Fitshirt.Domain.Exceptions;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Posts;
using Fitshirt.Infrastructure.Models.Posts.Entities;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Common.Entites;
using Fitshirt.Infrastructure.Repositories.Posts;
using Fitshirt.Infrastructure.Repositories.Users;

namespace Fitshirt.Domain.Features.Posts;

public class PostDomain : IPostDomain
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IColorRepository _colorRepository;
    private readonly ISizeRepository _sizeRepository;

    public PostDomain(IPostRepository postRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, IColorRepository colorRepository, ISizeRepository sizeRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        _colorRepository = colorRepository;
        _sizeRepository = sizeRepository;
    }

    public Task<bool> AddAsync(Post entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, Post entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingPost = await _postRepository.GetByIdAsync(id);

        if (existingPost == null)
        {
            throw new NotFoundException(nameof(Post), id);
        }

        return await _postRepository.DeleteAsync(id);
    }

    public async Task<bool> AddPostAsync(Post post, ICollection<int> sizeIds)
    {
        var user = await _userRepository.GetByIdAsync(post.UserId);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), post.UserId);
        }

        var category = await _categoryRepository.GetByIdAsync(post.CategoryId);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), post.CategoryId);
        }

        var color = await _colorRepository.GetByIdAsync(post.ColorId);

        if (color == null)
        {
            throw new NotFoundException(nameof(Color), post.ColorId);
        }

        var sizes = await _sizeRepository.GetSizesByIdsAsync(sizeIds);

        if (sizes.Count != sizeIds.Count)
        {
            throw new AnyNotFoundException(nameof(Size));
        }

        post.PostSizes = sizes.Select(s => new PostSize
        {
            Size = s
        }).ToList();

        return await _postRepository.AddAsync(post);
    }

    public async Task<bool> UpdatePostAsync(int id, Post post, ICollection<int> sizeIds)
    {
        var existingPost = await _postRepository.GetByIdAsync(id);

        if (existingPost == null)
        {
            throw new NotFoundException(nameof(Post), id);
        }
        
        var user = await _userRepository.GetByIdAsync(post.UserId);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), post.UserId);
        }

        var category = await _categoryRepository.GetByIdAsync(post.CategoryId);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), post.CategoryId);
        }
        
        var color = await _colorRepository.GetByIdAsync(post.ColorId);

        if (color == null)
        {
            throw new NotFoundException(nameof(Color), post.ColorId);
        }
        
        var sizes = await _sizeRepository.GetSizesByIdsAsync(sizeIds);

        if (sizes.Count != sizeIds.Count)
        {
            throw new AnyNotFoundException(nameof(List<Size>));
        }

        post.PostSizes = sizes.Select(s => new PostSize
        {
            Size = s
        }).ToList();

        return await _postRepository.UpdateAsync(id, post);
    }
}