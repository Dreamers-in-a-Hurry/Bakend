using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fitshirt.Infrastructure.Repositories.Posts;

public class PostRepository : IPostRepository
{
    private readonly FitshirtDbContext _context;

    public PostRepository(FitshirtDbContext context, ILogger<PostRepository> logger)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Post>> GetAllAsync()
    {
        return await _context.Posts.Where(post => post.IsEnable)
            .ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts
            .Where(post => post.IsEnable && post.Id == id)
            .Include(post => post.Category)
            .Include(post => post.Color)
            .Include(post => post.User)
            .Include(post => post.PostSizes)
            .ThenInclude(postSize => postSize.Size)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> AddAsync(Post post)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
        
        return true;
    }

    public async Task<bool> UpdateAsync(int id, Post entity)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var postToUpdate = _context.Posts.FirstOrDefault(post => post.Id == id);

            postToUpdate!.Name = entity.Name;
            postToUpdate.Image = entity.Image;
            postToUpdate.Stock = entity.Stock;
            postToUpdate.Price = entity.Price;
            postToUpdate.CategoryId = entity.CategoryId;
            postToUpdate.UserId = entity.UserId;
            postToUpdate.ColorId = entity.ColorId;
            postToUpdate.IsEnable = entity.IsEnable;
            postToUpdate.PostSizes = entity.PostSizes;
          
            _context.Posts.Update(postToUpdate);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var postToDelete = _context.Posts.FirstOrDefault(post => post.Id == id);

            if (postToDelete != null) postToDelete.IsEnable = false;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }

        return true;
    }

    public async Task<IReadOnlyCollection<Post>> GetPostsByUserId(int userId)
    {
        return await _context.Posts
            .Where(post => post.UserId == userId && post.IsEnable == true)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Post>> SearchByFiltersAsync (int? categoryId, int? colorId)
    {
        return await _context.Posts
            .Where(post =>
                (categoryId==null || post.CategoryId == categoryId) &&
                (colorId==null || post.ColorId == colorId) &&
                post.IsEnable
            )
            .ToListAsync();
    }
}