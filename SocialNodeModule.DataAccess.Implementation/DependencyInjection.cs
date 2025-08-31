using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNodeModule.DataAccess.Implementation.CloudStorage;
using SocialNodeModule.DataAccess.Implementation.Repositories;
using SocialNodeModule.DataAccess.Interfaces;
using SocialNodeModule.DataAccess.Interfaces.Repositories;

namespace SocialNodeModule.DataAccess.Implementation;

public static class DependencyInjection
{
    public static void AddSocialNodeDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CloudStorageSettings>(configuration.GetSection("CloudStorageConfig"));

        services.AddScoped<CloudStorageContext>();
        
        services.AddDbContext<SocialNodeDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SocialAppDb")));
        
        services.AddTransient(typeof(IBaseSocialNodeRepository<>), typeof(BaseSocialNodeRepository<>));
        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IPlaceRepository, PlaceRepository>();
        services.AddTransient<IClusterRepository, ClusterRepository>();
    }

    public static void UseSocialNodeDataAccess(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<SocialNodeDbContext>();
        dbContext?.Database.Migrate();
    }
}