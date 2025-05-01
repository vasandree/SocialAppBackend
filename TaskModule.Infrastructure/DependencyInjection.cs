using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskModule.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<TaskDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("SocialAppDb")));
    }

    public static void UseInfrastructure(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<TaskDbContext>();
        dbContext?.Database.Migrate();
    }
}