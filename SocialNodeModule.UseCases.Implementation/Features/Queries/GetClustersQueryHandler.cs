using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Contracts.Dtos;
using Shared.Domain.Exceptions;
using Shared.Extensions.Configs;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.ClusterOfPeople;
using SocialNodeModule.UseCases.Interfaces.Queries;

namespace SocialNodeModule.UseCases.Implementation.Features.Queries;

internal sealed class GetClustersQueryHandler(
    IClusterRepository clusterRepository,
    IMapper mapper,
    IOptions<PaginationConfig> config)
    : IRequestHandler<GetClustersQuery, PaginatedClusterDto>
{
    private readonly int _pageSize = config.Value.PageSize;

    public async Task<PaginatedClusterDto> Handle(GetClustersQuery request, CancellationToken cancellationToken)
    {
        var clusters = await clusterRepository.GetAllByUserId(request.UserId);

        if (!string.IsNullOrEmpty(request.Name))
        {
            clusters = clusters.Where(person => EF.Functions.Like(person.Name, $"%{request.Name}%"));
        }

        if (request.Page <= 0) throw new BadRequest("Page must be greater than 0");

        var totalCount = await clusters.CountAsync(cancellationToken);
        var totalPages = Math.Max(1, (int)Math.Ceiling((double)totalCount / _pageSize));

        clusters = clusters
            .Skip((request.Page - 1) * _pageSize)
            .Take(_pageSize);

        return new PaginatedClusterDto(mapper.Map<List<ListedBaseSocialNodeDto>>(clusters),new Pagination(_pageSize, request.Page, totalPages));
    }
}