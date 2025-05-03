using Shared.Extensions.Configs;

namespace SocialApp.Api.Extensions.Configurations;

public static class Configurations
{
    public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        var socialNetworkBaseUrlsConfig =  configuration
            .GetSection("SocialNetworkBaseUrlsConfig")
            .Get<SocialNetworkBaseUrlsConfig>();

        if (socialNetworkBaseUrlsConfig?.SocialNetworkBaseUrls == null || !socialNetworkBaseUrlsConfig.SocialNetworkBaseUrls.Any())
        {
            throw new InvalidOperationException("Failed to bind SocialNetworkBaseUrls configuration.");
        }
        
        services.Configure<PaginationConfig>(configuration.GetSection("Pagination"));
        services.Configure<SocialNetworkBaseUrlsConfig>(configuration.GetSection("SocialNetworkBaseUrlsConfig"));
    }
}