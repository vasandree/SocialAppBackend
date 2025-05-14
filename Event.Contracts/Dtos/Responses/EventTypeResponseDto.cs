using System.ComponentModel.DataAnnotations;

namespace Event.Contracts.Dtos.Responses;

public record EventTypeResponseDto
{
    [Key] public Guid Id { get; init; }
    [Required] public string Name { get; init; }
}