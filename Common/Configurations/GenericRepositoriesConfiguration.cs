using Common.Repositories.BaseEntityRepository;
using Common.Repositories.GenericRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Configurations;

public static class GenericRepositoriesConfiguration
{
    public static void AddGenericRepository(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddTransient(typeof(IBaseEntityRepository<>), typeof(BaseEntityRepository<>));
    }
}