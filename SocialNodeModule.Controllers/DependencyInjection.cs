using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNodeModule.UseCases.Implementation;
using SocialNodeModule.DataAccess.Implementation;

namespace SocialNodeModule.Controllers;

public static class DependencyInjection
{
    public static void AddSocialNodeModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSocialNodeUseCases();
        services.AddSocialNodeDataAccess(configuration);
    }

    public static void UseSocialNodeModule(this IServiceProvider services)
    {
        services.UseSocialNodeDataAccess();
    }
}