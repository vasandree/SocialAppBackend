using Microsoft.AspNetCore.Http;

namespace SocialNode.Contracts.Services;

public interface ICloudStorageService
{
    Task<string> UploadFileAsync(IFormFile file, Guid id);
}