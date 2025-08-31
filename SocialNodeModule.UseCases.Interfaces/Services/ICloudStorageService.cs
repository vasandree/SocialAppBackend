using Microsoft.AspNetCore.Http;

namespace SocialNodeModule.UseCases.Interfaces.Services;

public interface ICloudStorageService
{
    Task<string> UploadFileAsync(IFormFile file, Guid id);
}