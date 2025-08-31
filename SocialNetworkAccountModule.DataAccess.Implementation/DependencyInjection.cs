using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAccountModule.DataAccess.Implementation.Initializer;
using SocialNetworkAccountModule.DataAccess.Implementation.Repositories;
using SocialNetworkAccountModule.DataAccess.Interfaces;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;

namespace SocialNetworkAccountModule.DataAccess.Implementation;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsDataAccess(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SocialNetworkAccountsDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SocialAppDb")));
        
        services.AddTransient<IDbInitializer, DbInitializer>();
        services.AddTransient<IPersonsAccountRepository, PersonsAccountRepository>();
        services.AddTransient<IUsersAccountRepository, UsersAccountRepository>();
        services.AddTransient<ISocialNetworkUrlsRepository, SocialNetworkUrlsRepository>();
    }

    public static async Task UseSocialNetworkAccountsDataAccess(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var serviceScope = services.CreateScope();
        var initializer = serviceScope.ServiceProvider.GetService<IDbInitializer>();
        if (initializer != null) await initializer.InitializeAsync(cancellationToken);
    }
}