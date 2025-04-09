using AutoMapper;
using Common.Models.Dtos;
using MediatR;
using Microsoft.Extensions.Configuration;
using PersonService.Application.Dtos.Responses;
using PersonService.Application.Dtos.Responses.ClusterOfPeople;
using PersonService.Persistence.Repositories.ClusterRepository;

namespace PersonService.Application.Features.Queries.GetClustersQuery;

public class GetClustersQueryHandler : IRequestHandler<GetClustersQuery, PaginatedClusterDto>
{
    private readonly IClusterRepository _clusterRepository;
    private readonly IMapper _mapper;
    private readonly int _pageSize;

    public GetClustersQueryHandler(IClusterRepository clusterRepository, IConfiguration configuration, IMapper mapper)
    {
        _clusterRepository = clusterRepository;
        _mapper = mapper;
        _pageSize = configuration.GetValue<int>("PageSize");
    }

    public async Task<PaginatedClusterDto> Handle(GetClustersQuery request, CancellationToken cancellationToken)
    {
        var clusters = await _clusterRepository.GetAllAsQueryable(request.UserId);

        if (!string.IsNullOrEmpty(request.Name))
        {
            clusters = clusters.Where(cluster => cluster.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }
        
        var totalPages = (int)Math.Ceiling((double)clusters.Count() / _pageSize);
        
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