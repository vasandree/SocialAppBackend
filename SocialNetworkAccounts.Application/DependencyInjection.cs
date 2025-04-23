using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetworkAccounts.Application;

public static class DependencyInjection
{
    public static void AddSocialNetworkAccountsApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        builder.Services.AddAutoMapper(typeof(MappingProfile));
    }
}