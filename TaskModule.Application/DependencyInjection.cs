using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TaskModule.Application;

public static class DependencyInjection
{
    public static void AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        builder.Services.AddAutoMapper(typeof(MappingProfile));
    }
}