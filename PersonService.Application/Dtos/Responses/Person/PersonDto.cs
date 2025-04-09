using PersonService.Domain.Enums;

namespace PersonService.Application.Dtos.Responses.Person;

public class PersonDto : BaseSocialNodeDto
{
    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
}