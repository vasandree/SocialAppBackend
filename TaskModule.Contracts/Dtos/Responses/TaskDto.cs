using System.ComponentModel.DataAnnotations;

namespace TaskModule.Contracts.Dtos.Responses;

public class TaskDto
{
    [Required] public required Guid Id { get; init; }

    [Required] public required string Name { get; init; }

    public string? Description { get; init; }

    [Required] public required DateTime EndDate { get; init; }

    [Required] public required TaskStatus Status { get; init; } = TaskStatus.Created;

    [Required] public required Guid SocialNodeId { get; init; }
}