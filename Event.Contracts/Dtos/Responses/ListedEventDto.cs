using System.ComponentModel.DataAnnotations;

namespace Event.Contracts.Dtos.Responses;

public class ListedEventDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string? Location { get; set; }

    public EventTypeResponseDto? EventType { get; set; }

    [Required] public DateTime Date { get; set; }
    
}