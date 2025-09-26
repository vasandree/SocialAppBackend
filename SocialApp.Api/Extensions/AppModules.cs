using AuthModule.Controllers;
using EventModule.Controllers;
using SocialNetworkAccountModule.Controllers;
using SocialNodeModule.Controllers;
using TaskModule.Controllers;
using UserModule.Controllers;
using WorkspaceModule.Controllers;
using NotificationModule.Controllers;

namespace SocialApp.Api.Extensions;

public static class AppModules
{
    public static void AddAppModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNotificationModule(configuration);
        services.AddUserModule(configuration);
        services.AddAuthModule(configuration);
        services.AddSocialNetworkAccountsModule(configuration);
        services.AddSocialNodeModule(configuration);
        services.AddTaskModule(configuration);
        services.AddEventModule(configuration);
        services.AddWorkspaceModule(configuration);
    }

    public static async Task UseAppModules(this IServiceProvider services)
    {
        services.UseNotificationModule();
        services.UseUserModule();
        services.UseAuthModule();
        await services.UseSocialNetworkAccountsModule();
        services.UseSocialNodeModule();
        services.UseTaskModule();
        services.UseEventModule();
        services.UseWorkspaceModule();
    }
}