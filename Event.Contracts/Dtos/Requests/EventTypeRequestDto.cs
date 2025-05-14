using System.ComponentModel.DataAnnotations;

namespace Event.Contracts.Dtos.Requests;

public abstract record EventTypeRequestDto
{
    [Required]
    public string Name { get; set; }
}