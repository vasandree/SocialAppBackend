using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.UseCases.Implementation;
using UserModule.DataAccess.Implementation;

namespace UserModule.Controllers;

public static class DependencyInjection
{
    public static void AddUserModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddUserModuleUseCases();
        services.AddUserModuleDataAccess(configuration);
    }

    public static void UseUserModule(this IServiceProvider services)
    {
        services.UseUserModuleDataAccess();
    }
}