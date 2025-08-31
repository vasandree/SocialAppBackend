using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskModule.DataAccess.Implementation.Repositories;
using TaskModule.DataAccess.Interfaces;
using TaskModule.DataAccess.Interfaces.Repositories;

namespace TaskModule.DataAccess.Implementation;

public static class DependencyInjection
{
    public static void AddTaskModuleDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SocialAppDb")));
        
        services.AddTransient<ITaskRepository, TaskRepository>();
    }

    public static void UseTaskModuleDataAccess(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<TaskDbContext>();
        dbContext?.Database.Migrate();
    }
}