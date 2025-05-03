using System.ComponentModel.DataAnnotations;

namespace SocialNode.Contracts.Dtos.Responses;

public abstract class BaseSocialNodeDto
{
    [Required] public Guid Id { get; init; }

    [Required] public required string Name { get; init; }

    public string? Description { get; init; }

    public string? AvatarUrl { get; init; }
}