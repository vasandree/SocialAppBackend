using Common.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAccounts.Persistence.Repositories.PersonsAccountRepository;
using SocialNetworkAccounts.Persistence.Repositories.UsersAccountRepository;

namespace SocialNetworkAccounts.Persistence;

public static class SocialNetworkAccountsPersistenceConfiguration
{
    public static void AddSocialNetworkAccountsPersistence(this WebApplicationBuilder builder)
    {
        builder.AddGenericRepository();
        builder.Services.AddTransient<IPersonsAccountRepository, PersonsAccountRepository>();
        builder.Services.AddTransient<IUsersAccountRepository, UsersAccountRepository>();
        
        builder.Services.AddDbContext<SocialNetworkAccountsDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("SocialNetworkAccountsDb")));
    }
    
    public static void UseSocialNetworkAccountsPersistence(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<SocialNetworkAccountsDbContext>();
        dbContext?.Database.Migrate();
    }
}