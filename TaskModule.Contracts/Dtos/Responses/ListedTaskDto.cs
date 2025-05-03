using System.ComponentModel.DataAnnotations;

namespace TaskModule.Contracts.Dtos.Responses;

public class ListedTaskDto
{
    [Required] public required Guid Id { get; init; }

    [Required] public required string Name { get; init; }

    [Required] public required DateTime EndDate { get; init; }

    [Required] public required Guid SocialNodeId { get; init; }
}