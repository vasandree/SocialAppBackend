using EventModule.DataAccess.Implementation.Repositories;
using EventModule.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventModule.DataAccess.Implementation;

public static class DependencyInjection
{
    public static void AddEventModuleDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EventDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SocialAppDb")));
        
        services.AddTransient<IEventEntityRepository, EventEntityRepository>();
        services.AddTransient<IEventTypeRepository, EventTypeRepository>();
    }
    
    public static void UseEventModuleDataAccess(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<EventDbContext>();
        dbContext?.Database.Migrate();
    }
}