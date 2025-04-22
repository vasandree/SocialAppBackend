using Common.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using User.Contracts.Repositories;
using User.Persistence.Repositories;

namespace User.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<ITelegramAccountRepository, TelegramAccountRepository>();
    }
}