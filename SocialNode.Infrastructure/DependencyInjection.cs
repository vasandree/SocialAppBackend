using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNode.Contracts.Services;
using SocialNode.Infrastructure.CloudStorage;

namespace SocialNode.Infrastructure;

public static class DependencyInjection
{
    public static void AddSocialNodeInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        CloudServiceConfig.Initialize();
        var cloudinary = CloudServiceConfig.GetCloudinaryInstance();
        services.AddSingleton(cloudinary);

        services.AddScoped<ICloudStorageService, CloudStorageServiceService>();

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