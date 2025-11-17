using UserModule.Domain.Entities;

namespace TelegramClient.Interfaces;

public interface ITelegramClient
{
    Task<bool> SendAsync(string payloadJson, UserSettings settings, CancellationToken cancellationToken = default);
}