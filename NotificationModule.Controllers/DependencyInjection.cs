using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationModule.BackgroundJobs;
using NotificationModule.DataAccess.Implementation;
using NotificationModule.HttpClient.Implementation;

namespace NotificationModule.Controllers;

public static class DependencyInjection
{
    public static void AddNotificationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessImplementationServices(configuration);
        services.AddNotificationModuleBackgroundJobs(configuration);
        services.AddNotificationHttpClient();
    }
    
    public static void UseNotificationModule(this IServiceProvider services)
    {
        services.UseDataAccessImplementationServices();
    }
}