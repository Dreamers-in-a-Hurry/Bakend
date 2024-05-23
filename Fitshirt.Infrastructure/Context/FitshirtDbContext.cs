using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Context;

public class FitshirtDbContext : DbContext
{
    public FitshirtDbContext(DbContextOptions options) : base(options)
    {
    }
    
    // Add DbSet
}