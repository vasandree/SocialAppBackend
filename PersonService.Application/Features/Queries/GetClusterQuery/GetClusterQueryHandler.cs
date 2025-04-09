using AutoMapper;
using Common.Exceptions;
using MediatR;
using PersonService.Application.Dtos.Responses.ClusterOfPeople;
using PersonService.Persistence.Repositories.ClusterRepository;

namespace PersonService.Application.Features.Queries.GetClusterQuery;

public class GetClusterQueryHandler : IRequestHandler<GetClusterQuery, ClusterOfPeopleDto>
{
    private readonly IClusterRepository _clusterRepository;
    private readonly IMapper _mapper;

    public GetClusterQueryHandler(IClusterRepository clusterRepository, IMapper mapper)
    {
        _clusterRepository = clusterRepository;
        _mapper = mapper;
    }

    public async Task<ClusterOfPeopleDto> Handle(GetClusterQuery request, CancellationToken cancellationToken)
    {
        if (!await _clusterRepository.CheckIfExists(request.ClusterId))
            throw new NotFound("Provided cluster does not exist");

        if (!await _clusterRepository.CheckIfUserIsCreator(request.UserId, request.ClusterId))
            throw new Forbidden("You are not allowed to view this cluster");

        var cluster = await _clusterRepository.GetByIdAsync(request.ClusterId);
        return _mapper.Map<ClusterOfPeopleDto>(cluster);
    }
}