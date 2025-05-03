using Auth.Controllers;
using Event.Controllers;
using SocialNetworkAccounts.Controllers;
using SocialNode.Controllers;
using TaskModule.Controllers;
using User.Controllers;

namespace SocialApp.Api.Extensions;

public static class AppModules
{
    public static void AddAppModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddUserModule(configuration);
        services.AddAuthModule(configuration);
        services.AddSocialNetworkAccountsModule(configuration);
        services.AddSocialNodeModule(configuration);
        services.AddTaskModule(configuration);
        services.AddEventModule(configuration);
    }

    public static async Task UseAppModules(this IServiceProvider services)
    {
        services.UseUserModule();
        services.UseAuthModule();
        await services.UseSocialNetworkAccountsModule();
        services.UseSocialNodeModule();
        services.UseTaskModule();
        services.UseEventModule();
    }
}