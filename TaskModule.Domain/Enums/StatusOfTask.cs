using System.Text.Json.Serialization;

namespace TaskModule.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusOfTask
{
    Created,
    InProgress,
    Done,
    Cancelled
}