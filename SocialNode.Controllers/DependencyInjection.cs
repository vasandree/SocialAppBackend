using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNode.Application;
using SocialNode.Infrastructure;
using SocialNode.Persistence;

namespace SocialNode.Controllers;

public static class DependencyInjection
{
    public static void AddSocialNodeModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSocialNodeApplication();
        services.AddSocialNodePersistence();
        services.AddSocialNodeInfrastructure(configuration);
    }

    public static void UseSocialNodeModule(this IServiceProvider services)
    {
        services.UseSocialNodeInfrastructure();
    }
}