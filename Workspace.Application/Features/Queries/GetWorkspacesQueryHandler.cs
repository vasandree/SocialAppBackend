using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Shared.Contracts.Dtos;
using Shared.Domain.Exceptions;
using Shared.Extensions.Configs;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Contracts.Queries;
using Workspace.Contracts.Repositories;

namespace Workspace.Application.Features.Queries;

public class GetWorkspacesQueryHandler : IRequestHandler<GetWorkspacesQuery, WorkspacesDto>
{
    private readonly int _size;
    private readonly IMapper _mapper;
    private readonly IWorkspaceEntityRepository _workspaceRepository;

    public GetWorkspacesQueryHandler(IMapper mapper, IWorkspaceEntityRepository workspaceRepository,
        IOptions<PaginationConfig> config)
    {
        _size = config.Value.PageSize;
        _mapper = mapper;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<WorkspacesDto> Handle(GetWorkspacesQuery request, CancellationToken cancellationToken)
    {
        var workspaces = await _workspaceRepository.GetQueryableAsync();

        workspaces = workspaces.Where(x => x.CreatorId == request.UserId);

        if (request.Page <= 0) throw new BadRequest("Page must be greater than 0");

        var skip = (request.Page - 1) * _size;

        var totalPages = Math.Max(1, (int)Math.Ceiling((double)workspaces.Count() / _size));

        if (request.Page > totalPages) throw new BadRequest("Page must be less than or equal to the number of total pages");

        workspaces = workspaces
            .Skip((request.Page - 1) * _size)
            .Take(_size);

        return new WorkspacesDto()
        {
            Workspaces = _mapper.Map<IReadOnlyList<ListedWorkspaceDto>>(workspaces),
            Pagination = new Pagination(_size, request.Page, totalPages)
        };
    }
}