using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetworkAccounts.Infrastructure;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<SocialNetworkAccountsDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("SocialAppDb")));
    }
    
    public static void UseSocialNetworkAccountsInfrastructure(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<SocialNetworkAccountsDbContext>();
        dbContext?.Database.Migrate();
    }
}