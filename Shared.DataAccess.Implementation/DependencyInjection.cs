using Microsoft.Extensions.DependencyInjection;
using Shared.DataAccess.Implementation.Repositories;
using Shared.DataAccess.Interfaces;

namespace Shared.DataAccess.Implementation;

public static class DependencyInjection
{
    public static void AddSharedDataAccess(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient(typeof(IBaseEntityRepository<>), typeof(BaseEntityRepository<>));
    }
}