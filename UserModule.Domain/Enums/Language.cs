using System.Text.Json.Serialization;

namespace UserModule.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Language
{
    Ru,
    En
}