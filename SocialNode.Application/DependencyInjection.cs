using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNode.Application;

public static class DependencyInjection
{
    public static void AddSocialNodeApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        builder.Services.AddAutoMapper(typeof(MappingProfile));
    }
}