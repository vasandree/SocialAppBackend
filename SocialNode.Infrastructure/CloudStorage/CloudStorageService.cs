using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Services;

namespace SocialNode.Infrastructure.CloudStorage;

public class CloudStorageService : ICloudStorageService
{
    private readonly Cloudinary _cloudinary;

    public CloudStorageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary ??
                      throw new ArgumentNullException(nameof(cloudinary), "Cloudinary instance cannot be null.");
    }

    public async Task<string> UploadFileAsync(IFormFile file, Guid userId)
    {
        if (file == null || file.Length == 0)
        {
            throw new BadRequest("No file uploaded.");
        }

        try
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = $"user_avatar_{userId}",
                    Folder = "user_avatars"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.SecureUrl != null)
                    return uploadResult.SecureUrl.ToString();

                throw new BadRequest("Upload failed.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during file upload: {ex.Message}");
            throw;
        }
    }
}