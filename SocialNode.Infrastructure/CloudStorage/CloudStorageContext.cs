using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace SocialNode.Infrastructure.CloudStorage;

public class CloudStorageContext : IDisposable
{
    public readonly IMinioClient Client;

    public CloudStorageContext(IOptions<CloudStorageSettings> options)
    {
        var settings = options.Value;
        Client = new MinioClient()
            .WithEndpoint(settings.Endpoint, 9000)
            .WithCredentials(settings.User, settings.Password)
            .Build();

        InitializeBucketAsync().Wait();
    }

    private async Task InitializeBucketAsync()
    {
        await Client.MakeBucketAsync(new MakeBucketArgs().WithBucket("avatars"));
    }

    public void Dispose()
    {
        Client.Dispose();
    }
}