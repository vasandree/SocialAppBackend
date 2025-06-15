using System.Text.Json.Serialization;

namespace User.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Theme
{
    Dark = 1,
    Light = 2
}