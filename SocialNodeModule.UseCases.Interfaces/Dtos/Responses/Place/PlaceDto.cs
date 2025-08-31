namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Place;

public record PlaceDto(Guid Id, string Name, string? Description, string? AvatarUrl)
    : BaseSocialNodeDto(Id, Name, Description, AvatarUrl);