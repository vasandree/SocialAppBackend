namespace TaskModule.Contracts.Dtos.Responses;

public class TasksDto
{
    public IReadOnlyList<ListedTaskDto> OpenTasks { get; set; }
    public IReadOnlyList<ListedTaskDto> InProgressTasks { get; set; }
    public IReadOnlyList<ListedTaskDto> CompletedTasks { get; set; }
    public IReadOnlyList<ListedTaskDto> CancelledTasks { get; set; }
}