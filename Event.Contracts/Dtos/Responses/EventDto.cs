using System.ComponentModel.DataAnnotations;
using SocialNode.Contracts.Dtos.Responses;

namespace Event.Contracts.Dtos.Responses;

public record EventDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string? Location { get; set; }

    public EventTypeResponseDto? EventType { get; set; }

    [Required] public DateTime Date { get; set; }

    [Required] public required BaseSocialNodeDto SocialNode { get; set; }

    [Required] public Guid WorkspaceId { get; set; }
};