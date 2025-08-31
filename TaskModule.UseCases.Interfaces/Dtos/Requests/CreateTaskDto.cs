namespace TaskModule.UseCases.Interfaces.Dtos.Requests;

public record CreateTaskDto(
    string Name,
    string? Description,
    DateTime StartDate,
    DateTime EndDate,
    Guid SocialNodeId,
    Guid WorkspaceId);