using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetworkAccounts.Application;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsApplication(this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(typeof(MappingProfile));
    }
}