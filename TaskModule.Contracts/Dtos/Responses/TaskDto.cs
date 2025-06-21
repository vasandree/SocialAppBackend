using System.ComponentModel.DataAnnotations;
using TaskModule.Domain.Enums;

namespace TaskModule.Contracts.Dtos.Responses;

public class TaskDto
{
    [Required] public required Guid Id { get; init; }

    [Required] public required string Name { get; init; }

    public string? Description { get; init; }

    [Required] public required DateTime StartDate { get; init; }

    [Required] public required DateTime EndDate { get; init; }

    [Required] public required StatusOfTask Status { get; init; }

    [Required] public required Guid SocialNodeId { get; init; }

    [Required] public required Guid WorkspaceId { get; init; }
}