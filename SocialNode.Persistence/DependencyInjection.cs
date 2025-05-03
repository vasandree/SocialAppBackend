using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SocialNode.Contracts.Repositories;
using SocialNode.Persistence.Repositories;

namespace SocialNode.Persistence;

public static class DependencyInjection
{
    public static void AddSocialNodePersistence(this IServiceCollection services)

    {
        services.AddTransient(typeof(ISocialNodeRepository<>), typeof(SocialNodeRepository<>));
        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IPlaceRepository, PlaceRepository>();
        services.AddTransient<IClusterRepository, ClusterRepository>();
    }
}