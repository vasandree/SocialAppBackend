namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses;

public record ListedBaseSocialNodeDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? AvatarUrl { get; init; }
};