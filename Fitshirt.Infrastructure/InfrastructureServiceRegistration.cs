using Fitshirt.Infrastructure.Context;
using Fitshirt.Infrastructure.Repositories.Common.Entites;
using Fitshirt.Infrastructure.Repositories.Posts;
using Fitshirt.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fitshirt.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FitShirtConnection");

        services.AddDbContext<FitshirtDbContext>(options =>
        {
            options.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString)
            );
        });
        
        // Add Repositories
        // services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IColorRepository, ColorRepository>();
        services.AddScoped<ISizeRepository, SizeRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        
        return services;
    }
}