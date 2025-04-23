using MediatR;
using SocialNode.Contracts.Dtos.Responses.ClusterOfPeople;

namespace SocialNode.Contracts.Queries;

public record GetClusterQuery(Guid ClusterId, Guid UserId) : IRequest<ClusterOfPeopleDto>;