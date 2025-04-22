using Microsoft.AspNetCore.Builder;
using User.Application;
using User.Infrastructure;
using User.Persistence;

namespace User.Controllers;

public static class DependencyInjection
{
    public static void AddUserModule(this WebApplicationBuilder builder)
    {
        builder.AddApplication();
        builder.AddPersistence();
        builder.AddInfrastructure();
    }

    public static void UseUserModule(this WebApplication app)
    {
        app.UseInfrastructure();
    }
}