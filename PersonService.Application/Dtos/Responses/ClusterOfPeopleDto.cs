using System.ComponentModel.DataAnnotations;
using PersonService.Domain.Enums;

namespace PersonService.Application.Dtos.Responses;

public class ClusterOfPeopleDto : BaseSocialNodeDto
{
    [Required]
    public List<PersonDto> Persons { get; set;}
    
    public SocialNodeType Type => SocialNodeType.ClusterOfPeople;

}