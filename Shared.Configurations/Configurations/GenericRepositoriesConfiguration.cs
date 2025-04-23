using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Repositories;
using Shared.Persistence.Repositories;

namespace Shared.Configurations.Configurations;

public static class GenericRepositoriesConfiguration
{
    public static void AddGenericRepository(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddTransient(typeof(IBaseEntityRepository<>), typeof(BaseEntityRepository<>));
    }
}