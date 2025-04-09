using System.ComponentModel.DataAnnotations;
using PersonService.Application.Dtos.Responses.Person;

namespace PersonService.Application.Dtos.Responses.ClusterOfPeople;

public class ClusterOfPeopleDto : BaseSocialNodeDto
{
    [Required]
    public List<PersonDto> Persons { get; set;}
    
}