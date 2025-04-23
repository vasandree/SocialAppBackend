using Microsoft.AspNetCore.Builder;
using SocialNetworkAccounts.Application;
using SocialNetworkAccounts.Infrastructure;
using SocialNetworkAccounts.Persistence;

namespace SocialNetworkAccounts.Controllers;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsModule(this WebApplicationBuilder builder)
    {
        builder.AddSocialNetworkAccountsApplication();
        builder.AddSocialNetworkAccountsInfrastructure();
        builder.AddSocialNetworkAccountsPersistence();
    }
    
    public static void UseSocialNetworkAccountsModule(this WebApplication application)
    {
        application.UseSocialNetworkAccountsInfrastructure();
    }
}