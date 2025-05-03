using System.ComponentModel.DataAnnotations;
using Shared.Domain;
using TaskModule.Domain.Enums;

namespace TaskModule.Domain.Entites;

public class TaskEntity : BaseEntity
{
    [Required] public required string Name { get; set; }

    public string? Description { get; set; }

    [Required] public DateTime CreateDate { get; init; } = DateTime.UtcNow;

    [Required] public required DateTime EndDate { get; set; }

    [Required] public StatusOfTask Status { get; set; } = StatusOfTask.Created;

    [Required] public required Guid SocialNodeId { get; set; }

    [Required] public required Guid CreatorId { get; set; }
}