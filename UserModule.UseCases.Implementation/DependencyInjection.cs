using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using UserModule.UseCases.Implementation.Features.Commands.AddUser.Factory;
using UserModule.UseCases.Implementation.Helpers;
using UserModule.UseCases.Interfaces.Helpers;

namespace UserModule.UseCases.Implementation;

public static class DependencyInjection
{
    public static void AddUserModuleUseCases(this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<ITelegramHelper, TelegramHelper>();
        services.AddScoped<IAddUserCommandFactory, AddUserCommandFactory>();
        services.AddAutoMapper(typeof(MappingProfile));
    }
}