using Microsoft.AspNetCore.Http;
using Minio.DataModel.Args;
using SocialNode.Contracts.Services;
using SocialNode.Infrastructure.CloudStorage;

namespace SocialNode.Application;

public class CloudStorageService : ICloudStorageService
{
    private readonly CloudStorageContext _context;

    public CloudStorageService(CloudStorageContext context)
    {
        _context = context;
    }

    public async Task<string> UploadFileAsync(IFormFile file, Guid id)
    {
        var objectName = id.ToString();

        await using var stream = file.OpenReadStream();

        var encodedName = Uri.EscapeDataString(objectName);

        var args = new PutObjectArgs()
            .WithBucket("avatars")
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType)
            .WithHeaders(new Dictionary<string, string> { { "Name", encodedName } });

        await _context.Client.PutObjectAsync(args);

        var url = await _context.Client.PresignedGetObjectAsync(
            new PresignedGetObjectArgs()
                .WithBucket("avatars")
                .WithObject(objectName)
                .WithExpiry(60 * 60)
        );

        return url;
    }
}