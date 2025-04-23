using System.Text.Json.Serialization;

namespace Shared.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SocialNetwork
{
    Facebook,
    Twitter,
    LinkedIn,
    Instagram,
    YouTube,
    Pinterest,
    Snapchat,
    TikTok,
    Reddit,
    WhatsApp,
    GitHub,
    Telegram,
    Twitch,
    Vk
}