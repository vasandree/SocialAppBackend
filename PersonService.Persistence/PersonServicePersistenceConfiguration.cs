using Common.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Persistence;

public static class PersonServicePersistenceConfiguration
{
    public static void AddPersonServicePersistence(this WebApplicationBuilder builder)
    
    {
        builder.AddGenericRepository();
        builder.Services.AddTransient<ISocialNodeRepository, SocialNodeRepository>();


        builder.Services.AddDbContext<PersosnsDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));
    }

    public static void AddPersonServicePersistence(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<PersosnsDbContext>();
        dbContext?.Database.Migrate();
    }
}