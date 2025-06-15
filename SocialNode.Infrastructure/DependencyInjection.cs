using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNode.Infrastructure.CloudStorage;

namespace SocialNode.Infrastructure;

public static class DependencyInjection
{
    public static void AddSocialNodeInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CloudStorageSettings>(configuration.GetSection("CloudStorageConfig"));

        services.AddScoped<CloudStorageContext>();
        
        services.AddDbContext<SocialNodeDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SocialAppDb")));
    }

    public static void UseSocialNodeInfrastructure(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<SocialNodeDbContext>();
        dbContext?.Database.Migrate();
    }
}