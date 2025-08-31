using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using UserModule.UseCases.Implementation.Helpers;
using UserModule.UseCases.Interfaces.Helpers;

namespace UserModule.UseCases.Implementation;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<ITelegramHelper, TelegramHelper>();
        services.AddAutoMapper(typeof(MappingProfile));
    }
}