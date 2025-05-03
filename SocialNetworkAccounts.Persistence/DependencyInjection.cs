using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNetworkAccounts.Persistence.Repositories;

namespace SocialNetworkAccounts.Persistence;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsPersistence(this IServiceCollection services)
    {
        services.AddTransient<IPersonsAccountRepository, PersonsAccountRepository>();
        services.AddTransient<IUsersAccountRepository, UsersAccountRepository>();
        services.AddTransient<ISocialNetworkUrlsRepository, SocialNetworkUrlsRepository>();
    }
}