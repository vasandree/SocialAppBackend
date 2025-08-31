namespace EventModule.UseCases.Interfaces.Dtos.Requests;

public record UpdateEventDto(
    string Title,
    string? Description,
    string? Location,
    Guid? EventTypeId,
    DateTime Date,
    List<Guid> SocialNodeId);