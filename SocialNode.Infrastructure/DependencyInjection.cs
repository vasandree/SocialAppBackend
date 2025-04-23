using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNode.Contracts.Services;
using SocialNode.Infrastructure.CloudStorage;

namespace SocialNode.Infrastructure;

public static class DependencyInjection
{
    public static void AddSocialNodeInfrastructure(this WebApplicationBuilder builder)
    {
        CloudServiceConfig.Initialize();
        var cloudinary = CloudServiceConfig.GetCloudinaryInstance();
        builder.Services.AddSingleton(cloudinary);

        builder.Services.AddScoped<ICloudStorageService, CloudStorageServiceService>();

        builder.Services.AddDbContext<SocialNodeDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("SocialAppDb")));
    }
    
    public static void UseSocialNodeInfrastructure(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<SocialNodeDbContext>();
        dbContext?.Database.Migrate();
    }
}