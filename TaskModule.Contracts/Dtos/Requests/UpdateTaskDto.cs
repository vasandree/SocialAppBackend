using System.ComponentModel.DataAnnotations;

namespace TaskModule.Contracts.Dtos.Requests;

public abstract record UpdateTaskDto
{
    [Required] public required string Name { get; init; }

    public string? Description { get; init; }

    [Required] public required DateTime StartDate { get; init; }

    [Required] public required DateTime EndDate { get; init; }

    [Required] public required Guid SocialNodeId { get; init; }
}