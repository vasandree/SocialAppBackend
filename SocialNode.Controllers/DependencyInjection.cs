using Microsoft.AspNetCore.Builder;
using SocialNode.Application;
using SocialNode.Infrastructure;
using SocialNode.Persistence;

namespace SocialNode.Controllers;

public static class DependencyInjection
{
    public static void AddSocialNodeModule(this WebApplicationBuilder builder)
    {
        builder.AddSocialNodeApplication();
        builder.AddSocialNodePersistence();
        builder.AddSocialNodeInfrastructure();
    }

    public static void UseSocialNodeModule(this WebApplication app)
    {
        app.UseSocialNodeInfrastructure();
    }
}