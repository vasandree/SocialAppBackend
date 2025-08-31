using AuthModule.DataAccess.Implementation.Repositories;
using AuthModule.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthModule.DataAccess.Implementation;

public static class DependencyInjection
{
    public static void AddAuthModuleDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SocialAppDb")));
        
        services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
    }
    
    public static void UseAuthModuleDataAccess(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AuthDbContext>();
        dbContext?.Database.Migrate();
    }
}