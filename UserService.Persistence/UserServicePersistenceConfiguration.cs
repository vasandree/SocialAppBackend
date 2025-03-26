using Common.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Persistence.Repositories.RefreshTokenRepository;
using UserService.Persistence.Repositories.TelegramAccountRepository;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Persistence;

public static class UserServicePersistenceConfiguration
{
    public static void AddUserServicePersistence(this WebApplicationBuilder builder)
    {
        builder.AddGenericRepository();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        builder.Services.AddTransient<ITelegramAccountRepository, TelegramAccountRepository>();

        builder.Services.AddDbContext<UserServiceDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));
    }

    public static void UseUserServicePersistence(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<UserServiceDbContext>();
        dbContext?.Database.Migrate();
    }
}