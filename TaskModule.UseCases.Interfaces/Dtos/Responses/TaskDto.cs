namespace TaskModule.UseCases.Interfaces.Dtos.Responses;

public class TaskDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime StartDate,
    DateTime EndDate,
    TaskStatus Status,
    Guid SocialNodeId,
    Guid WorkspaceId);