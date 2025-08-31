using System.Text.Json.Serialization;

namespace WorkspaceModule.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SocialNodeType
{
    Place,
    Person,
    ClusterOfPeople
}