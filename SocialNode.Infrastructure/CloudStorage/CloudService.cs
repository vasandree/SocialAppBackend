using CloudinaryDotNet;
using dotenv.net;
using Microsoft.Extensions.Configuration;

namespace SocialNode.Infrastructure.CloudStorage;

public static class CloudService
{
    private static Cloudinary? _cloudinary;

    public static void Initialize(IConfiguration configuration)
    {
        try
        {
            var config = GetConfig(configuration);

            if (config == null)
            {
                throw new Exception("Cloudinary configuration not found.");
            }

            var account = new Account(
                config.CloudName,
                config.ApiKey,
                config.ApiSecret
            );

            _cloudinary = new Cloudinary(account)
            {
                Api = { Secure = true }
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during Cloudinary initialization: {ex.Message}");
            throw;
        }
    }

    public static Cloudinary GetCloudinaryInstance()
    {
        if (_cloudinary == null)
        {
            throw new InvalidOperationException(
                "Cloudinary has not been initialized. Call Initialize(configuration) first.");
        }

        return _cloudinary;
    }

    private static CloudStorageConfig? GetConfig(IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment == "Development")
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true, ignoreExceptions: false));

            var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
            var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");
            var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret) ||
                string.IsNullOrEmpty(cloudName))
            {
                throw new Exception("API Key, Secret or Cloud name is missing in Development.");
            }

            return new CloudStorageConfig()
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret,
                CloudName = cloudName
            };
        }

        return configuration.GetSection("CloudStorageConfig").Get<CloudStorageConfig>();
    }
}