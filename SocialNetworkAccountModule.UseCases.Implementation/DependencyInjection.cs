using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetworkAccountModule.UseCases.Implementation;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsUseCases(this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
    }
}