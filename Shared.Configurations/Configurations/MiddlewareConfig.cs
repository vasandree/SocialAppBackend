using Microsoft.AspNetCore.Builder;

namespace Shared.Configurations.Configurations;

public static class MiddlewareConfig
{
    public static void UseMiddleware(this WebApplication app)
    {
        app.UseMiddleware<Common.Middleware.Middleware>();
    }
}