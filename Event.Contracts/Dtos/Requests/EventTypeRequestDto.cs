using System.ComponentModel.DataAnnotations;

namespace Event.Contracts.Dtos.Requests;

public class EventTypeRequestDto
{
    [Required]
    public string Name { get; set; }
}