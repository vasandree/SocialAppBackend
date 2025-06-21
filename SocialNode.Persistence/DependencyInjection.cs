using Microsoft.Extensions.DependencyInjection;
using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using SocialNode.Persistence.Repositories;

namespace SocialNode.Persistence;

public static class DependencyInjection
{
    public static void AddSocialNodePersistence(this IServiceCollection services)

    {
        services.AddTransient(typeof(IBaseSocialNodeRepository<>), typeof(BaseSocialNodeRepository<>));
        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IPlaceRepository, PlaceRepository>();
        services.AddTransient<IClusterRepository, ClusterRepository>();
    }
}