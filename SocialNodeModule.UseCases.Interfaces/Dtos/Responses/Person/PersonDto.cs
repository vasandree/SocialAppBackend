namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;

public record PersonDto(
    Guid Id,
    string Name,
    string? Description,
    string? AvatarUrl,
    string? Email,
    string? PhoneNumber)
    : BaseSocialNodeDto(Id, Name, Description, AvatarUrl);