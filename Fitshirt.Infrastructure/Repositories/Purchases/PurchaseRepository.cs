using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Purchases;
using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Repositories.Purchases;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly FitshirtDbContext _context;

    public PurchaseRepository(FitshirtDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Purchase>> GetAllAsync()
    {
        return await _context.Purchases
            .Where(purchase => purchase.IsEnable)
            .Include(purchase => purchase.User)
            .Include(purchase => purchase.Items)
                .ThenInclude(item => item.Size)
            .Include(purchase => purchase.Items)
                .ThenInclude(item => item.Post)
            .ToListAsync();
    }

    public async Task<Purchase?> GetByIdAsync(int id)
    {
        return await _context.Purchases
            .Where(purchase => purchase.IsEnable && purchase.Id == id)
            .Include(purchase => purchase.User)
            .Include(purchase => purchase.Items)
                .ThenInclude(item => item.Size)
            .Include(purchase => purchase.Items)
                .ThenInclude(item => item.Post)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> AddAsync(Purchase entity)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Purchases.Add(entity);
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

    public Task<bool> UpdateAsync(int id, Purchase entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<Purchase>> GetPurchasesByUserId(int userId)
    {
        return await _context.Purchases
            .Where(purchase => purchase.IsEnable && purchase.UserId == userId)
            .Include(purchase => purchase.User)
            .Include(purchase => purchase.Items)
                .ThenInclude(item => item.Size)
            .Include(purchase => purchase.Items)
                .ThenInclude(item => item.Post)
            .ToListAsync();
    }
}