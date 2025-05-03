namespace SocialApp.Api.Extensions;

public static class Logging
{
    public static void AddLoggingConfiguration(this IServiceCollection services)
    {
        services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddDebug();
        });
    }
}