using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SocialNode.Contracts.Dtos.Requests;

public class PlaceRequestDto
{
    [Required] public required string Name { get; init; }

    public string? Description { get; init; }

    public IFormFile? Avatar { get; init; }
}