using EventModule.UseCases.Interfaces.Dtos.Responses;
using TaskModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

public record WorkspaceResponseDto
{
    public WorkspaceInfoDto Info { get; init; }
    public TasksDto Tasks { get; init; }
    public IReadOnlyList<ListedEventDto> Events { get; init; }
};