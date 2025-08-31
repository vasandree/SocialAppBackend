using AuthModule.DataAccess.Implementation;
using AuthModule.UseCases.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthModule.Controllers;

public static class DependencyInjection
{
    public static void AddAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthModuleUseCases();
        services.AddAuthModuleDataAccess(configuration);
    }
    
    public static void UseAuthModule(this IServiceProvider services)
    {
        services.UseAuthModuleDataAccess();
    }
}