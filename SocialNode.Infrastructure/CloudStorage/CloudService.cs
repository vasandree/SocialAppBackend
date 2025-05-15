using CloudinaryDotNet;
using dotenv.net;
using Microsoft.Extensions.Options;

namespace SocialNode.Infrastructure.CloudStorage;

public class CloudService
{
    private static Cloudinary? _cloudinary;
    private static IOptions<CloudStorageConfig>? _cloudStorageConfig;

    public CloudService(IOptions<CloudStorageConfig>? cloudStorageConfig)
    {
        _cloudStorageConfig = cloudStorageConfig;
    }

    public static void Initialize()
    {
        try
        {
            var config = GetConfig();


            if (config != null)
            {
                Account account = new Account(
                    config.CloudName,
                    config.ApiKey,
                    config.ApiSecret
                );

                _cloudinary = new Cloudinary(account);
                _cloudinary.Api.Secure = true;
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during initialization: {ex.Message}");
            throw;
        }
    }

    public static Cloudinary? GetCloudinaryInstance()
    {
        if (_cloudinary == null)
        {
            throw new InvalidOperationException("Cloudinary has not been initialized. Call Initialize() first.");
        }

        return _cloudinary;
    }

    private static CloudStorageConfig? GetConfig()
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
                throw new Exception("API Key or API Secret or Cloud name is not set.");
            }

            return new CloudStorageConfig()
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret,
                CloudName = cloudName,
            };
        }
        else
        {
            if (_cloudStorageConfig != null) return _cloudStorageConfig.Value;
        }

        return null;
    }
}