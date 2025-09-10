using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;

namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses.ClusterOfPeople;

public record ClusterOfPeopleDto : BaseSocialNodeDto
{
    public List<PersonDto> Persons { get; init; }
}