using System.Reflection;
using Fitshirt.Api.Errors;

namespace Fitshirt.Api;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}