using Fitshirt.Domain.Features.Designs;
using Fitshirt.Domain.Features.Posts;
using Microsoft.Extensions.DependencyInjection;

namespace Fitshirt.Domain;

public static class DomainServiceRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // Add domains
        // services.AddScoped<IBookDomain, BookDomain>();
        services.AddScoped<IPostDomain, PostDomain>();
        services.AddScoped<IDesignDomain, DesignDomain>();

        return services;
    }
}