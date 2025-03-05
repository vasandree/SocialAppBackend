using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Helpers.JwtService;
using UserService.Application.Helpers.TelegramHelper;

namespace UserService.Application;

public static class UserServiceApplicationConfiguration
{
    public static void AddUserServiceApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddScoped<ITelegramHelper, TelegramHelper>();
        builder.Services.AddScoped<IJwtService, JwtService>();
    }
}