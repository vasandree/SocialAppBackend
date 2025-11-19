using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SocialNodeModule.UseCases.Interfaces.Services;
using SocialNodeModule.UseCases.Implementation.Services;

namespace SocialNodeModule.UseCases.Implementation;

public static class DependencyInjection
{
    public static void AddSocialNodeUseCases(this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

        services.AddScoped<ICloudStorageService, CloudStorageService>();
        services.AddScoped<ISocialNodeService, SocialNodeService>();
    }
}