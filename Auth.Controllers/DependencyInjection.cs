using Auth.Application;
using Auth.Infrastructure;
using Auth.Persistence;
using Microsoft.AspNetCore.Builder;

namespace Auth.Controllers;

public static class DependencyInjection
{
    public static void AddAuthModule(this WebApplicationBuilder builder)
    {
        builder.AddApplication();
        builder.AddPersistence();
        builder.AddInfrastructure();
    }
    
    public static void UseAuthModule(this WebApplication app)
    {
        app.UseInfrastructure();
    }
}