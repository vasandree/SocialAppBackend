using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SocialNode.Contracts.Dtos.Requests;

public class PersonRequestDto
{
    [Required] public string Name { get; init; }

    public string? Description { get; }

    public string? Email { get; }

    public string? PhoneNumber { get; }

    public IFormFile? Avatar { get; init; }

    public string? AvatarUrl { get; init; }
}