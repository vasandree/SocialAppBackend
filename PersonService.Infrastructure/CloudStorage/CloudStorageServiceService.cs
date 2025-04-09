using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace PersonService.Infrastructure.CloudStorage;

public class CloudStorageServiceService : ICloudStorageService
{
    private readonly Cloudinary _cloudinary;

    public CloudStorageServiceService(Cloudinary cloudinary)
    {
        if (cloudinary == null)
        {
            throw new ArgumentNullException(nameof(cloudinary), "Cloudinary instance cannot be null.");
        }

        _cloudinary = cloudinary;
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