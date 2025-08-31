using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;

namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses.ClusterOfPeople;

public record ClusterOfPeopleDto(Guid Id, string Name, string? Description, string? AvatarUrl, List<PersonDto> Persons)
    : BaseSocialNodeDto(Id, Name, Description, AvatarUrl);