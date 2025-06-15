using Microsoft.Extensions.DependencyInjection;
using User.Contracts.Repositories;
using User.Persistence.Repositories;

namespace User.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITelegramAccountRepository, TelegramAccountRepository>();
        services.AddTransient<IUserSettingsRepository, UserSettingsRepository>();
    }
}