using Microsoft.AspNetCore.Http;
using Minio.DataModel.Args;
using SocialNode.Contracts.Services;
using SocialNode.Infrastructure.CloudStorage;

namespace SocialNode.Application.Services;

public class CloudStorageService : ICloudStorageService
{
    private readonly CloudStorageContext _context;

    public CloudStorageService(CloudStorageContext context)
    {
        _context = context;
    }

    public async Task<string> UploadFileAsync(IFormFile file, Guid id)
    {
        const string bucketName = "avatars";
        var objectName = id.ToString();

        var bucketExists = await _context.Client.BucketExistsAsync(
            new BucketExistsArgs().WithBucket(bucketName)
        );

        if (!bucketExists)
        {
            await _context.Client.MakeBucketAsync(
                new MakeBucketArgs().WithBucket(bucketName)
            );

            var policyJson = $@"
    {{
      ""Version"": ""2012-10-17"",
      ""Statement"": [
        {{
          ""Effect"": ""Allow"",
          ""Principal"": {{ ""AWS"": [ ""*"" ] }},
          ""Action"": [ ""s3:GetObject"" ],
          ""Resource"": [ ""arn:aws:s3:::{bucketName}/*"" ]
        }}
      ]
    }}";

            await _context.Client.SetPolicyAsync(new SetPolicyArgs()
                .WithBucket(bucketName)
                .WithPolicy(policyJson)
            );
        }


        await using var stream = file.OpenReadStream();

        var putArgs = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType);

        await _context.Client.PutObjectAsync(putArgs);

        var url = $"{_context.BaseUrl}/{bucketName}/{objectName}";

        return url;
    }
}