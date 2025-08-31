using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.ClusterOfPeople;

namespace SocialNodeModule.UseCases.Interfaces.Queries;

public record GetClusterQuery(Guid ClusterId, Guid UserId) : IRequest<ClusterOfPeopleDto>;