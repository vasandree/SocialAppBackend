using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.DataAccess.Implementation.Repositories;
using UserModule.DataAccess.Interfaces;
using UserModule.DataAccess.Interfaces.Repositories;

namespace UserModule.DataAccess.Implementation;

public static class DependencyInjection
{
    public static void AddUserModuleDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SocialAppDb")));
        
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITelegramAccountRepository, TelegramAccountRepository>();
        services.AddTransient<IUserSettingsRepository, UserSettingsRepository>();
    }

    public static void UseUserModuleDataAccess(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<UserDbContext>();
        dbContext?.Database.Migrate();
    }
}