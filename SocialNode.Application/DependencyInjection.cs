using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SocialNode.Application.Services;
using SocialNode.Contracts.Services;

namespace SocialNode.Application;

public static class DependencyInjection
{
    public static void AddSocialNodeApplication(this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<ICloudStorageService, CloudStorageService>();
        services.AddScoped<ISocialNodeService, SocialNodeService>();
    }
}