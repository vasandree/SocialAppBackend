using Microsoft.AspNetCore.Http;

namespace PersonService.Infrastructure.CloudStorage;

public interface ICloudStorageService
{
    Task<string> UploadFileAsync (IFormFile file, Guid id);
}