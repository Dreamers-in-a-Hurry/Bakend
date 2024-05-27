using Microsoft.Extensions.DependencyInjection;

namespace Fitshirt.Domain;

public static class DomainServiceRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // Add domains
        // services.AddScoped<IBookDomain, BookDomain>();

        return services;
    }
}