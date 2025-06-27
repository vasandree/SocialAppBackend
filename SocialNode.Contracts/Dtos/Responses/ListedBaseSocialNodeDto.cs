using System.ComponentModel.DataAnnotations;

namespace SocialNode.Contracts.Dtos.Responses;

public class ListedBaseSocialNodeDto
{
    [Required] public Guid Id { get; init; }

    [Required] public required string Name { get; init; }
    
    public string? AvatarUrl { get; init; }
}