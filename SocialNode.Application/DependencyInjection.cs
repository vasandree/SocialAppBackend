using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNode.Application;

public static class DependencyInjection
{
    public static void AddSocialNodeApplication(this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddAutoMapper(typeof(MappingProfile));
    }
}