namespace TaskModule.UseCases.Interfaces.Dtos.Responses;

public class TasksDto(
    IReadOnlyList<ListedTaskDto> OpenTasks,
    IReadOnlyList<ListedTaskDto> InProgressTasks,
    IReadOnlyList<ListedTaskDto> CompletedTasks,
    IReadOnlyList<ListedTaskDto> CancelledTasks);