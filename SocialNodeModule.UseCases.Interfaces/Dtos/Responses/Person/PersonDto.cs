namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;

public record PersonDto : BaseSocialNodeDto
{
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}