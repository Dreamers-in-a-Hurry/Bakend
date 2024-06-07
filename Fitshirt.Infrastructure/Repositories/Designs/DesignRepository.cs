using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Designs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fitshirt.Infrastructure.Repositories.Designs;

public class DesignRepository : IDesignRepository
{
    private readonly FitshirtDbContext _context;

    public DesignRepository(FitshirtDbContext context, ILogger<DesignRepository> logger)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<Design>> GetAllAsync()
    {
        return await _context.Designs
            .Where(design => design.IsEnable)
            .ToListAsync();
    }

    public async Task<Design?> GetByIdAsync(int id)
    {
        return await _context.Designs
            .Where(design => design.IsEnable && design.Id == id)
            .Include(design => design.User )
            .Include(design => design.PrimaryColor)
            .Include(design => design.SecondaryColor)
            .Include(design => design.TertiaryColor)
            .Include(design => design.Shield)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> AddAsync(Design design)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Designs.Add(design);
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

    public async Task<bool> UpdateAsync(int id, Design entity)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var designToUpdate = _context.Designs.FirstOrDefault(design => design.Id == id);

            designToUpdate!.Name = entity.Name;
            designToUpdate.PrimaryColorId = entity.PrimaryColorId;
            designToUpdate.SecondaryColorId = entity.SecondaryColorId;
            designToUpdate.TertiaryColorId = entity.TertiaryColorId;
            designToUpdate.ShieldId = entity.ShieldId;

            _context.Designs.Update(designToUpdate);
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
            var designToDelete = _context.Designs.FirstOrDefault(design => design.Id == id);
            if (designToDelete != null) designToDelete.IsEnable = false;

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

    public async Task<IReadOnlyCollection<Design>> GetDesignsByUserId(int userId)
    {
        return await _context.Designs
            .Where(design => design.UserId == userId && design.IsEnable == true)
            .ToListAsync();
    }
}