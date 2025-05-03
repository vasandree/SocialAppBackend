namespace SocialApp.Api.Extensions.Middleware;

public static class DependencyInjection
{
    public static void UseMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}