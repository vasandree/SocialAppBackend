using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SocialNode.Contracts.Repositories;
using SocialNode.Persistence.Repositories;

namespace SocialNode.Persistence;

public static class DependencyInjection
{
    public static void AddSocialNodePersistence(this WebApplicationBuilder builder)

    {
        builder.Services.AddTransient(typeof(ISocialNodeRepository<>), typeof(SocialNodeRepository<>));
        builder.Services.AddTransient<IPersonRepository, PersonRepository>();
        builder.Services.AddTransient<IPlaceRepository, PlaceRepository>();
        builder.Services.AddTransient<IClusterRepository, ClusterRepository>();
    }
}