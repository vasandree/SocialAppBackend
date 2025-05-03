using Auth.Contracts.Repositories;
using Auth.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}