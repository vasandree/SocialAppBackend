using UserModule.Domain.Entities;

namespace NotificationModule.HttpClient.Interfaces;

public interface INotificationSenderClient
{
    Task<bool> SendAsync(string payloadJson, UserSettings settings, CancellationToken cancellationToken = default);
}