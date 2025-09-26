using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationModule.DataAccess.Interfaces;

namespace NotificationModule.DataAccess.Implementation;

public static class DependencyInjection
{
    public static void AddDataAccessImplementationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NotificationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SocialAppDb")));
        
        services.AddTransient<IOutboxMessageRepository, OutboxMessageRepository>();
    }
    
    public static void UseDataAccessImplementationServices(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        dbContext.Database.Migrate();
    }
}