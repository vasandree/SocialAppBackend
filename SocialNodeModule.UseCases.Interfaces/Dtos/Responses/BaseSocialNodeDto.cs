namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses;

public abstract record BaseSocialNodeDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? AvatarUrl { get; init; }
};