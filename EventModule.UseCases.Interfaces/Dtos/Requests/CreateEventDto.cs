namespace EventModule.UseCases.Interfaces.Dtos.Requests;

public record CreateEventDto(
    string Title,
    string? Description,
    string? Location,
    Guid? EventTypeId,
    DateTime Date,
    List<Guid> SocialNodeId,
    Guid WorkspaceId);