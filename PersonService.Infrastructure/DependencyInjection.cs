using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PersonService.Infrastructure.CloudStorage;

namespace PersonService.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        CloudServiceConfig.Initialize();
        var cloudinary = CloudServiceConfig.GetCloudinaryInstance();
        builder.Services.AddSingleton(cloudinary);

        builder.Services.AddScoped<ICloudStorageService, CloudStorageServiceService>();
    }
}