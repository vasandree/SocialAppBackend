namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses;

public abstract record BaseSocialNodeDto(Guid Id, string Name, string? Description, string? AvatarUrl);