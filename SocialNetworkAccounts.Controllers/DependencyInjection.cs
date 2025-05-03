using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAccounts.Application;
using SocialNetworkAccounts.Infrastructure;
using SocialNetworkAccounts.Persistence;

namespace SocialNetworkAccounts.Controllers;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSocialNetworkAccountsApplication();
        services.AddSocialNetworkAccountsInfrastructure(configuration);
        services.AddSocialNetworkAccountsPersistence();
    }
    
    public static async Task UseSocialNetworkAccountsModule(this IServiceProvider services)
    {
        await services.UseSocialNetworkAccountsInfrastructure();
    }
}