using Microsoft.AspNetCore.Http;
using Minio.DataModel.Args;
using SocialNodeModule.UseCases.Interfaces.Services;
using SocialNodeModule.DataAccess.Implementation.CloudStorage;

namespace SocialNodeModule.UseCases.Implementation.Services;

public class CloudStorageService(CloudStorageContext context) : ICloudStorageService
{
    public async Task<string> UploadFileAsync(IFormFile file, Guid id)
    {
        const string bucketName = "avatars";
        var objectName = id.ToString();

        var bucketExists = await context.Client.BucketExistsAsync(
            new BucketExistsArgs().WithBucket(bucketName)
        );

        if (!bucketExists)
        {
            await context.Client.MakeBucketAsync(
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

            await context.Client.SetPolicyAsync(new SetPolicyArgs()
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

        await context.Client.PutObjectAsync(putArgs);

        var url = $"{context.BaseUrl}/{bucketName}/{objectName}";

        return url;
    }
}