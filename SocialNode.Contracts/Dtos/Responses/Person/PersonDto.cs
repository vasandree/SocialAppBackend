namespace SocialNode.Contracts.Dtos.Responses.Person;

public class PersonDto : BaseSocialNodeDto
{
    public string? Email { get; init; }

    public string? PhoneNumber { get; init; }
}