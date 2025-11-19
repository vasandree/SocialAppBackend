using TelegramClient.Implementation;

namespace SocialApp.Api.Extensions;

public static class Clients
{
    public static void AddClients(this IServiceCollection services)
    {
        services.AddNotificationHttpClient();
    }
}