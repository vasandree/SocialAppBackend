using Microsoft.Extensions.Options;
using Minio;

namespace SocialNode.Infrastructure.CloudStorage;

public class CloudStorageContext : IDisposable
{
    public IMinioClient Client { get; }
    public string BaseUrl { get; }

    public CloudStorageContext(IOptions<CloudStorageSettings> options)
    {
        var settings = options.Value;

        Client = new MinioClient()
            .WithEndpoint(settings.Endpoint, 9000)
            .WithCredentials(settings.User, settings.Password)
            .Build();

        BaseUrl = $"http://{settings.Endpoint}:9000";
    }

    public void Dispose()
    {
        if (Client is IDisposable disposable)
            disposable.Dispose();
    }
}