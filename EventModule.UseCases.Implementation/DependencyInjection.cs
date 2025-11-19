using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EventModule.UseCases.Implementation;

public static class DependencyInjection
{
    public static void AddEventModuleUseCases(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}