using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Helpers;
using User.Contracts.Helpers;

namespace User.Application;

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