using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.ClusterOfPeople;
using SocialNodeModule.UseCases.Interfaces.Queries;

namespace SocialNodeModule.UseCases.Implementation.Features.Queries;

internal sealed class GetClusterQueryHandler(IClusterRepository clusterRepository, IMapper mapper)
    : IRequestHandler<GetClusterQuery, ClusterOfPeopleDto>
{
    public async Task<ClusterOfPeopleDto> Handle(GetClusterQuery request, CancellationToken cancellationToken)
    {
        if (!await clusterRepository.CheckIfUserIsCreator(request.UserId, request.ClusterId))
            throw new Forbidden("You are not allowed to view this cluster");

        var cluster = await clusterRepository.GetByIdAsync(request.ClusterId);
        return mapper.Map<ClusterOfPeopleDto>(cluster);
    }
}