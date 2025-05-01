using System.ComponentModel.DataAnnotations;
using SocialNode.Contracts.Dtos.Responses.Person;

namespace SocialNode.Contracts.Dtos.Responses.ClusterOfPeople;

public class ClusterOfPeopleDto : BaseSocialNodeDto
{
    [Required]
    public List<PersonDto> Persons { get; set;}
}