namespace SocialNode.Infrastructure.CloudStorage;

public class CloudStorageConfig
{
    public required string ApiKey { get; init; }
    public required string ApiSecret { get; init; }
    public required string CloudName { get; init; }
}