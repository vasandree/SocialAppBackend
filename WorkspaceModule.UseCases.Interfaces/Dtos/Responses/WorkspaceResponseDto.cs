using EventModule.UseCases.Interfaces.Dtos.Responses;
using TaskModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

public record WorkspaceResponseDto(WorkspaceInfoDto Info, TasksDto Tasks, IReadOnlyList<ListedEventDto> Events);