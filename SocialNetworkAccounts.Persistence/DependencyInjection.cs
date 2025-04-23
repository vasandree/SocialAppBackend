using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNetworkAccounts.Persistence.Repositories;

namespace SocialNetworkAccounts.Persistence;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IPersonsAccountRepository, PersonsAccountRepository>();
        builder.Services.AddTransient<IUsersAccountRepository, UsersAccountRepository>();
    }
}