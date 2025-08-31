using Microsoft.AspNetCore.Http;

namespace SocialNodeModule.UseCases.Interfaces.Dtos.Requests;

public record PersonRequestDto(
    string Name,
    string? Description,
    string? Email,
    string? PhoneNumber,
    IFormFile? Avatar,
    string? AvatarUrl);