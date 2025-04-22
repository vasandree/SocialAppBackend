using Auth.Contracts.Repositories;
using Auth.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}