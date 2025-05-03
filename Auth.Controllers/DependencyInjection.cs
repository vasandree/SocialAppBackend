using Auth.Application;
using Auth.Infrastructure;
using Auth.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Controllers;

public static class DependencyInjection
{
    public static void AddAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddPersistence();
        services.AddInfrastructure(configuration);
    }
    
    public static void UseAuthModule(this IServiceProvider services)
    {
        services.UseInfrastructure();
    }
}