namespace TaskModule.Contracts.Dtos.Responses;

public class TasksDto
{
    public required IReadOnlyList<ListedTaskDto> OpenTasks { get; init; }
    public required IReadOnlyList<ListedTaskDto> InProgressTasks { get; set; }
    public required IReadOnlyList<ListedTaskDto> CompletedTasks { get; set; }
    public required IReadOnlyList<ListedTaskDto> CancelledTasks { get; set; }
}