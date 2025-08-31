using System.Text.Json.Serialization;

namespace UserModule.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Theme
{
    Dark = 1,
    Light = 2
}