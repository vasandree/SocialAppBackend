using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Shared.Contracts.Dtos;
using Shared.Domain.Exceptions;
using Shared.Extensions.Configs;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;
using WorkspaceModule.UseCases.Interfaces.Queries;

namespace WorkspaceModule.UseCases.Implementation.Features.Queries;

internal sealed class GetWorkspacesQueryHandler(
    IMapper mapper,
    IWorkspaceEntityRepository workspaceRepository,
    IOptions<PaginationConfig> config)
    : IRequestHandler<GetWorkspacesQuery, WorkspacesDto>
{
    private readonly int _size = config.Value.PageSize;

    public async Task<WorkspacesDto> Handle(GetWorkspacesQuery request, CancellationToken cancellationToken)
    {
        var workspaces = workspaceRepository.GetQueryableAsync();

        workspaces = workspaces.Where(x => x.CreatorId == request.UserId);

        if (request.Page <= 0) throw new BadRequest("Page must be greater than 0");

        var skip = (request.Page - 1) * _size;

        var totalPages = Math.Max(1, (int)Math.Ceiling((double)workspaces.Count() / _size));

        if (request.Page > totalPages) throw new BadRequest("Page must be less than or equal to the number of total pages");

        workspaces = workspaces
            .Skip((request.Page - 1) * _size)
            .Take(_size);

        return new WorkspacesDto(mapper.Map<IReadOnlyList<ListedWorkspaceDto>>(workspaces),
            new Pagination(_size, request.Page, totalPages));
    }
}