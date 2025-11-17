using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared.Extensions.Configs;
using TelegramClient.Interfaces;
using UserModule.Domain.Entities;

namespace TelegramClient.Implementation;

public class TelegramClient(
    HttpClient httpClient,
    IOptions<NotificationHttpConfig> options,
    IConfiguration configuration) : ITelegramClient
{
    private readonly string _botToken =
        configuration.GetSection("TelegramBotApiKey").Value ?? throw new InvalidOperationException();


    public async Task<bool> SendAsync(string payloadJson, UserSettings settings,
        CancellationToken cancellationToken = default)
    {
        var cfg = options.Value;

        var endpoint = BuildEndpoint(cfg);
        if (string.IsNullOrWhiteSpace(endpoint)) return false;


        using var content = BuildHttpContent(cfg, payloadJson, settings);
        var response = await httpClient.PostAsync(endpoint, content, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    private string BuildEndpoint(NotificationHttpConfig cfg) => $"{cfg.BaseUrl}{_botToken}{cfg.EndpointPath}";


    private HttpContent BuildHttpContent(NotificationHttpConfig cfg, string payloadJson, UserSettings settings)
    {
        if (cfg.UseFormUrlEncoded)
        {
            var kv = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(settings.ChatInstance))
                kv.Add(new KeyValuePair<string, string>("chat_id", settings.ChatInstance));
            kv.Add(new KeyValuePair<string, string>("text", payloadJson));
            if (!string.IsNullOrWhiteSpace(cfg.ParseMode))
                kv.Add(new KeyValuePair<string, string>("parse_mode", cfg.ParseMode!));
            return new FormUrlEncodedContent(kv);
        }

        object body = !string.IsNullOrWhiteSpace(_botToken)
            ? new
            {
                chat_id = settings.ChatInstance,
                text = payloadJson,
                parse_mode = string.IsNullOrWhiteSpace(cfg.ParseMode) ? null : cfg.ParseMode
            }
            : new { text = payloadJson, parse_mode = cfg.ParseMode };

        var json = JsonSerializer.Serialize(body,
            new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}