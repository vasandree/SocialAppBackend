using Shared.Contracts.Repositories;
using Shared.Persistence.Repositories;

namespace SocialApp.Api.Extensions;

public static class GenericRepositories
{
    public static void AddGenericRepository(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient(typeof(IBaseEntityRepository<>), typeof(BaseEntityRepository<>));
    }
}