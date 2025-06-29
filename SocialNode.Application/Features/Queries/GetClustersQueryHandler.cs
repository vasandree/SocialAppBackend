using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Contracts.Dtos;
using Shared.Domain.Exceptions;
using Shared.Extensions.Configs;
using SocialNode.Contracts.Dtos.Responses;
using SocialNode.Contracts.Dtos.Responses.ClusterOfPeople;
using SocialNode.Contracts.Queries;
using SocialNode.Contracts.Repositories;

namespace SocialNode.Application.Features.Queries;

public class GetClustersQueryHandler : IRequestHandler<GetClustersQuery, PaginatedClusterDto>
{
    private readonly IClusterRepository _clusterRepository;
    private readonly IMapper _mapper;
    private readonly int _pageSize;

    public GetClustersQueryHandler(IClusterRepository clusterRepository, IMapper mapper,
        IOptions<PaginationConfig> config)
    {
        _clusterRepository = clusterRepository;
        _mapper = mapper;
        _pageSize = config.Value.PageSize;
    }

    public async Task<PaginatedClusterDto> Handle(GetClustersQuery request, CancellationToken cancellationToken)
    {
        var clusters = await _clusterRepository.GetAllByUserId(request.UserId);

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

        return new PaginatedClusterDto()
        {
            Cluster = _mapper.Map<List<ListedBaseSocialNodeDto>>(clusters),
            Pagination = new Pagination(_pageSize, request.Page, totalPages)
        };
    }
}