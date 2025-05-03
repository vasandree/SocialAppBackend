using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAccounts.Infrastructure.Initializer;

namespace SocialNetworkAccounts.Infrastructure;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SocialNetworkAccountsDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SocialAppDb")));

        services.AddTransient<IDbInitializer, DbInitializer>();
    }

    public static async Task UseSocialNetworkAccountsInfrastructure(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var initializer = serviceScope.ServiceProvider.GetService<IDbInitializer>();
        if (initializer != null) await initializer.InitializeAsync();
    }
}