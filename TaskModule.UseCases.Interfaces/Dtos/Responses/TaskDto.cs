namespace TaskModule.UseCases.Interfaces.Dtos.Responses;

public class TaskDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public TaskStatus Status { get; init; }
    public Guid SocialNodeId { get; init; }
    public Guid WorkspaceId { get; init; }
}