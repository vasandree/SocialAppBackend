namespace TaskModule.UseCases.Interfaces.Dtos.Responses;

public class TasksDto(
    List<ListedTaskDto> openTasksDto,
    List<ListedTaskDto> inProgressTasksDto,
    List<ListedTaskDto> completedTasksDto,
    List<ListedTaskDto> cancelledTasksDto)
{
    public IReadOnlyList<ListedTaskDto> OpenTasks { get; init; } = openTasksDto;
    public IReadOnlyList<ListedTaskDto> InProgressTasks { get; init; } = inProgressTasksDto;
    public IReadOnlyList<ListedTaskDto> CompletedTasks { get; init; } = completedTasksDto;
    public IReadOnlyList<ListedTaskDto> CancelledTasks { get; init; } = cancelledTasksDto;
}