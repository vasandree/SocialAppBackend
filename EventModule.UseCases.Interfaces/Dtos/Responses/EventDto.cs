using SocialNodeModule.UseCases.Interfaces.Dtos.Responses;

namespace EventModule.UseCases.Interfaces.Dtos.Responses;

public record EventDto(
    Guid Id,
    string Name,
    string? Location,
    EventTypeResponseDto? EventType,
    DateTime Date,
    BaseSocialNodeDto SocialNode,
    Guid WorkspaceId);