using Fitshirt.Infrastructure.Models.Common;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Models.Designs.Entities;
using Fitshirt.Infrastructure.Models.Posts;
using Fitshirt.Infrastructure.Models.Posts.Entities;
using Fitshirt.Infrastructure.Models.Purchases;
using Fitshirt.Infrastructure.Models.Purchases.Entities;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Models.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitshirt.Infrastructure.Context;

public class FitshirtDbContext : DbContext
{
    public FitshirtDbContext(DbContextOptions options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = 1;
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = 1;
                    entry.Entity.ModifiedDate = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>()
            .OwnsOne(user => user.Address,
                navigationBuilder =>
                {
                    navigationBuilder.Property(address => address.City)
                        .HasColumnName("City");
                    navigationBuilder.Property(address => address.Street)
                        .HasColumnName("Street");
                    navigationBuilder.Property(address => address.ZipCode)
                        .HasColumnName("ZipCode");
                }
            );

        builder.Entity<User>()
            .HasOne(user => user.DebitCard)
            .WithOne(card => card.User)
            .HasForeignKey<User>(user => user.DebitCardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<User>()
            .HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(user => user.RoleId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<User>()
            .HasOne(user => user.Service)
            .WithMany(service => service.Users)
            .HasForeignKey(user => user.ServiceId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<User>()
            .HasMany(user => user.Designs)
            .WithOne(design => design.User)
            .HasForeignKey(design => design.UserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<User>()
            .HasMany(user => user.Items)
            .WithOne(item => item.User)
            .HasForeignKey(item => item.UserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<User>()
            .HasMany(user => user.Posts)
            .WithOne(post => post.User)
            .HasForeignKey(post => post.UserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
        
        //-------

        builder.Entity<Post>()
            .HasOne(post => post.Category)
            .WithMany(category => category.Posts)
            .HasForeignKey(post => post.CategoryId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Post>()
            .HasOne(post => post.Color)
            .WithMany(color => color.Posts)
            .HasForeignKey(post => post.ColorId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Post>()
            .HasMany(post => post.PostSizes)
            .WithOne(ps => ps.Post)
            .HasForeignKey(ps => ps.PostId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Post>()
            .HasOne(post => post.User)
            .WithMany(user => user.Posts)
            .HasForeignKey(post => post.UserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);
        
        // ------

        builder.Entity<Design>()
            .HasOne(design => design.Shield)
            .WithMany(shield => shield.Designs)
            .HasForeignKey(design => design.ShieldId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Design>()
            .HasOne(design => design.User)
            .WithMany(user => user.Designs)
            .HasForeignKey(design => design.UserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Service> Services { get; set; }
    
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Item> Items { get; set; }
    
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PostSize> PostSizes { get; set; }
    
    public DbSet<Design> Designs { get; set; }
    public DbSet<Shield> Shields { get; set; }
    
    public DbSet<Color> Colors { get; set; }
    public DbSet<Size> Sizes { get; set; }
}