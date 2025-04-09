using Common.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonService.Persistence.Repositories.ClusterRepository;
using PersonService.Persistence.Repositories.PersonRepository;
using PersonService.Persistence.Repositories.PlaceRepository;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Persistence;

public static class DependencyInjection
{
    public static void AddPersonServicePersistence(this WebApplicationBuilder builder)
    
    {
        builder.AddGenericRepository();
        builder.Services.AddTransient(typeof(ISocialNodeRepository<>), typeof(SocialNodeRepository<>));
        builder.Services.AddTransient<IPersonRepository, PersonRepository>();
        builder.Services.AddTransient<IPlaceRepository, PlaceRepository>();
        builder.Services.AddTransient<IClusterRepository, ClusterRepository>();


        builder.Services.AddDbContext<PersosnsDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PersonDb")));
    }

    public static void AddPersonServicePersistence(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<PersosnsDbContext>();
        dbContext?.Database.Migrate();
    }
}