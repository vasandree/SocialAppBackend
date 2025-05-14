using System.ComponentModel.DataAnnotations;

namespace Event.Contracts.Dtos.Requests;

public abstract record UpdateEventDto
{
    [Required] public string Title { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public Guid? EventTypeId { get; set; }

    [Required] public DateTime Date { get; set; }
}