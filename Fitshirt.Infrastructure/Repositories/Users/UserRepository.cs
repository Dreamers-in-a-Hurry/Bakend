using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly FitshirtDbContext _context;

    public UserRepository(FitshirtDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await _context.Users.Where(user => user.IsEnable).ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.Where(user => user.IsEnable && user.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> AddAsync(User entity)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }

    }

    public async Task<bool> UpdateAsync(int id, User entity)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var userToDelete = _context.Users.FirstOrDefault(user => user.Id == id);

            userToDelete!.IsEnable = false;
            _context.Users.Update(userToDelete);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.IsEnable && u.Email == email);
    }

    public async Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.IsEnable && u.Cellphone == phoneNumber);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.IsEnable && u.Username == username);
    }
}