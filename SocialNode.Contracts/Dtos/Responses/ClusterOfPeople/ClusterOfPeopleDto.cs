using System.ComponentModel.DataAnnotations;
using SocialNode.Contracts.Dtos.Responses.Person;

namespace SocialNode.Contracts.Dtos.Responses.ClusterOfPeople;

public class ClusterOfPeopleDto : BaseSocialNodeDto
{
    [Required]
    public required List<PersonDto> Persons { get; init;}
}