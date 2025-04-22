using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Helpers;
using User.Contracts.Helpers;

namespace User.Application;

public static class DependencyInjection
{
    public static void AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITelegramHelper, TelegramHelper>();
        builder.Services.AddAutoMapper(typeof(MappingProfile));
    }
}