using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAccountModule.DataAccess.Implementation;
using SocialNetworkAccountModule.UseCases.Implementation;

namespace SocialNetworkAccountModule.Controllers;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSocialNetworkAccountsUseCases();
        services.AddSocialNetworkAccountsDataAccess(configuration);
    }
    
    public static async Task UseSocialNetworkAccountsModule(this IServiceProvider services)
    {
        await services.UseSocialNetworkAccountsDataAccess();
    }
}